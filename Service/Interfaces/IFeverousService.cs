using Microsoft.AspNetCore.Mvc;

namespace Service.Interfaces
{
    public interface IFeverousService
    {
        Task<IActionResult> GetFeverousByCustomerID(Guid customerID);
        Task<IActionResult> AddFeverous(Guid customerID, Guid productID);
        Task<IActionResult> DeleteFeverousItem(Guid customerId, Guid feverousItemID);
    }
}
