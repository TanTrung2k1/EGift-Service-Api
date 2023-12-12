using Data.Entities;
using Data.Models.Filters;
using Data.Models.Requests.Post;
using Data.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace Service.Interfaces
{
    public interface IVoucherService
    {
        Task<IActionResult> CreateVoucher(VoucherCreateModel vcm);
        Task<IActionResult> GetVouchers(VoucherFilterModel vfm, string customerId);
        Task<IActionResult> GetVoucher(Guid? id);
        Task<VoucherViewModel> GetVoucherByCode(string code);
        Task<IActionResult> RollBackVoucher(Guid? voucherId, Guid? customerId);
        Task<VoucherViewModel> CheckValidVoucher(string code, int price);
    }
}
