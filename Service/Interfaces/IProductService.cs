using Data.Models.Filters;
using Data.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Service.Interfaces
{
    public interface IProductService
    {
        Task<IActionResult> GetProducts(ProductFilterModel filter);
        Task<IActionResult> GetProduct(Guid id);
        Task<IActionResult> GetProductByCategoryId(Guid idCategory);
        Task<IActionResult> CreateProduct(ProductCreateModel productCreateModel);
        Task<IActionResult> UpdateProduct(Guid ProductId, ProductUpdateModel productUpdateModel);
        Task<IActionResult> RemoveProduct(Guid idProduct);
        Task<IActionResult> GetProductImage(Guid idProduct);
    }
}
