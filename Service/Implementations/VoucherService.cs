using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Models.Filters;
using Data.Models.Requests.Post;
using Data.Models.View;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Interfaces;

namespace Service.Implementations
{
    public class VoucherService : BaseService, IVoucherService
    {
        private readonly IConfiguration _configuration;
        private readonly IVoucherRepository _voucherRepository;
        private string _code;
        public VoucherService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : base(unitOfWork, mapper)
        {
            _configuration = configuration;
            _voucherRepository = unitOfWork.Voucher;
            _code = _configuration.GetSection("VoucherCode").Value!.ToString();
        }

        public async Task<IActionResult> CreateVoucher(VoucherCreateModel vcm)
        {
            Voucher voucher = new Voucher();
            voucher.Code = await GenerateCodeVoucher();
            voucher.FromPrice = vcm.FromPrice;
            voucher.ToPrice = vcm.ToPrice;
            voucher.Quantity = vcm.Quantity;
            voucher.Description = vcm.Description;
            voucher.Discount = vcm.Discount;
            voucher.CreateAt = DateTime.Now;
            voucher.StartDate = vcm.StartDate;
            voucher.EndDate = vcm.EndDate;
            voucher.Id = Guid.NewGuid();
            voucher.IsActive = true;
            _voucherRepository.Add(voucher);
            await _unitOfWork.SaveChanges();
            return await GetVoucher(voucher.Id);
        }

        private async Task InvalidVoucher()
        {
            var voucher = _voucherRepository.GetMany(x => x.IsActive);
            if (voucher != null)
            {
                foreach (var item in voucher)
                {
                    if ((DateTime.Now.DayOfYear - item.EndDate.DayOfYear) >= 0)
                    {
                        if ((DateTime.Now.Hour - item.EndDate.Hour) >= 0)
                        {
                            item.IsActive = false;
                        }
                    }
                    if (item.Quantity <= 0)
                    {
                        item.IsActive = false;
                    }
                }
                await _unitOfWork.SaveChanges();
            }
        }

        public async Task<IActionResult> GetVoucher(Guid? id)
        {
            var voucher = await _voucherRepository.FirstOrDefaultAsync(x => x.Id.Equals(id));
            return new JsonResult(_mapper.Map<VoucherViewModel>(voucher));
        }

        public async Task<IActionResult> GetVouchers(VoucherFilterModel vfm, string customerId)
        {
            await InvalidVoucher();
            var query = _voucherRepository.GetWithInlude(x => x.IsActive, x => x.Customers);
            List<Voucher> voucherList = new List<Voucher>();
            if (vfm.Price != null)
            {
                query = query.Where(x => vfm.Price >= x.FromPrice && vfm.Price <= x.ToPrice);
            }
            if (customerId != null)
            {
                foreach (var item in query)
                {
                    var j = item.Customers.Where(x => x.Id.Equals(Guid.Parse(customerId))).FirstOrDefault();
                    if (item.Customers.Where(x => x.Id.Equals(Guid.Parse(customerId))).FirstOrDefault() != null!)
                    {
                        voucherList.Add(item);
                    }
                }
            }
            foreach (var item in voucherList)
            {
                query = query.Where(x => x != item);
            }
            return new JsonResult(query.ProjectTo<VoucherViewModel>(_mapper.ConfigurationProvider));
        }

        #region CodeVoucher
        private async Task<string> GenerateCodeVoucher()
        {
            Random random = new Random();
            string codeVoucher = "";
            while (true)
            {
                for (int i = 0; i < 10; i++)
                {
                    codeVoucher += _code.Substring(random.Next(0, _code.Length), 1);
                }
                if (await _voucherRepository.FirstOrDefaultAsync(x => x.Code.Equals(codeVoucher)) is null)
                {
                    break;
                }
            }
            return codeVoucher;
        }

        #endregion


        public async Task<VoucherViewModel> GetVoucherByCode(string code)
        {
            var voucher = await _voucherRepository.FirstOrDefaultAsync(x => x.Code.Equals(code));
            return voucher != null ? _mapper.Map<VoucherViewModel>(voucher) : null!;
        }

        public async Task<IActionResult> RollBackVoucher(Guid? voucherId, Guid? customerId)
        {
            var voucher = await _voucherRepository.FirstOrDefaultInlude(x => x.Id.Equals(voucherId), x => x.Customers);
            voucher.IsActive = true;
            voucher.Customers.Remove(voucher.Customers.Where(x => x.Id == customerId).FirstOrDefault()!);
            voucher.Quantity += 1;
            await _unitOfWork.SaveChanges();
            return await GetVoucher(voucherId);
        }

        public async Task<VoucherViewModel> CheckValidVoucher(string code, int price)
        {
            await InvalidVoucher();
            var voucher = await GetVoucherByCode(code);
            if (voucher != null && price >= voucher.FromPrice && price <= voucher.ToPrice)
            {
                return voucher;
            }
            return null!;
        }
    }
}
