using Data.Models.Internal;
using Data.Models.Requests.Get;
using Data.Models.View;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Service.Interfaces
{
    public interface IPaymentService
    {
        string GeneratePaymentUrl(PaymentModel paymentModel);
        Task<IpnResponseViewModel> ProcessIpnCallback(IpnModel ipnModel, IQueryCollection queries);


    }
}
