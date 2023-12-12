
using AutoMapper;
using Data;
using Data.Entities;
using Data.Models.Internal;
using Data.Models.Requests.Get;
using Data.Models.View;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Service.Interfaces;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Utility.Enums;
using Utility.Settings;

namespace Service.Implementations
{
    public class PaymentService : BaseService, IPaymentService
    {
        private readonly AppSetting _appSetting;


        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<AppSetting> appSettingOptions) : base(unitOfWork, mapper)
        {
            _appSetting = appSettingOptions.Value;
        }

        private string EncodeToHmacSHA512(string key, String inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {


                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }
        public string GeneratePaymentUrl(PaymentModel paymentModel)
        {

            //350.000 ==> 350000(00)
            int amountByRemovedFraction = paymentModel.Amount * 100;

            string encodedCallback = WebUtility.UrlEncode($"https://gift.com/count-redirect-payment");

            string encodedOrderInfo = WebUtility.UrlEncode(_appSetting.VnPay.OrderInfo);

            string encodedOrderId = WebUtility.UrlEncode(paymentModel.OrderId.ToString());

            string query = $"vnp_Amount={amountByRemovedFraction}&vnp_BankCode=VNBANK&vnp_Command=pay&vnp_CreateDate={DateTime.Now.ToString("yyyyMMddHHmmss")}&vnp_CurrCode=VND&vnp_IpAddr={paymentModel.IpAddress}&vnp_Locale=vn&vnp_OrderInfo={encodedOrderInfo}&vnp_OrderType=other&vnp_ReturnUrl={encodedCallback}&vnp_TmnCode={_appSetting.VnPay.TmnCode}&vnp_TxnRef={encodedOrderId}&vnp_Version=2.1.0";

            string hash = EncodeToHmacSHA512(_appSetting.VnPay.HashSecret, query);

            string paymentUrl = $"{_appSetting.VnPay.Url}?{query}&vnp_SecureHash={hash}";

            return paymentUrl;
        }

        private string QueriesToRawQueryString(IQueryCollection queries)
        {
            StringBuilder rawResponse = new StringBuilder();

            foreach(var query in queries)
            {
                if (query.Key.IsNullOrEmpty() || 
                    query.Value.IsNullOrEmpty() || 
                    !query.Key.StartsWith("vnp_") ||
                    query.Key == "vnp_SecureHashType" ||
                    query.Key == "vnp_SecureHash")
                {
                    continue;
                }

                rawResponse.Append($"{WebUtility.UrlEncode(query.Key)}={WebUtility.UrlEncode(query.Value)}&");
            }

            rawResponse.Remove(rawResponse.Length - 1, 1);

            return rawResponse.ToString();
        }

        private bool ValidateHash(IpnModel model, IQueryCollection queries)
        {
            var rawResponse = QueriesToRawQueryString(queries);

            var encodedResponse = EncodeToHmacSHA512(_appSetting.VnPay.HashSecret, rawResponse);

            return encodedResponse.Equals(model.vnp_SecureHash);
        }

        public async Task<IpnResponseViewModel> ProcessIpnCallback(IpnModel ipnModel, IQueryCollection queries)
        {

            var ipnResponseViewModel = new IpnResponseViewModel();

            if (!ValidateHash(ipnModel, queries))
            {

                ipnResponseViewModel.RspCode = "99";
                ipnResponseViewModel.Message = ResponseMessage.ValidateHashError;

                return ipnResponseViewModel;
            }

            var transaction = await _unitOfWork.TransactionRepository.FirstOrDefaultAsync(t => t.TxnRef == ipnModel.vnp_TxnRef);

            if (transaction != null)
            {

                ipnResponseViewModel.RspCode = "02";
                ipnResponseViewModel.Message = ResponseMessage.TxnRefExist;

                return ipnResponseViewModel;
            }

            var orderGuid = new Guid(ipnModel.vnp_TxnRef);

            var order = await _unitOfWork.Order.FirstOrDefaultAsync(o => o.Id == orderGuid);

            if (order == null)
            {
                ipnResponseViewModel.RspCode = "01";
                ipnResponseViewModel.Message = ResponseMessage.OrderNotFound;

                return ipnResponseViewModel;
            }


            //convert back to normal amount
            ipnModel.vnp_Amount /= 100; 

            if (order.Amount != ipnModel.vnp_Amount) {

                ipnResponseViewModel.RspCode = "04";
                ipnResponseViewModel.Message = ResponseMessage.AmountNotValid;

                return ipnResponseViewModel;
            }

            
            if (ipnModel.vnp_ResponseCode == "00" && ipnModel.vnp_TransactionStatus == "00")
            {
                order.Status = OrderStatus.Transport; // should state is Transport?
                //How can we know when order is paid?? 
                //database is missing something...


                var transactional = new Transaction
                {
                    Amount = ipnModel.vnp_Amount,
                    OrderId = orderGuid,
                    TxnRef = ipnModel.vnp_TxnRef
                };
                _unitOfWork.TransactionRepository.Add(transactional);

            }

            await _unitOfWork.SaveChanges();

            ipnResponseViewModel.RspCode = "00";
            ipnResponseViewModel.Message = ResponseMessage.PaymentSuccess;

            return ipnResponseViewModel;
        }
    }
}
