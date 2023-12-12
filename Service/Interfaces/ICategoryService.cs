using Data.Entities;
using Data.Models.Filters;
using Data.Models.Request;
using Data.Models.Requests.Put;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICategoryService
    {
        Task<IActionResult> GetCategory(CategoryFilterModel categoryFilter);
        Task<IActionResult> GetCategoryById(Guid id);
        Task<IActionResult> CreateCategory(CategoryCreateModel categoryCreateModel);
        Task<IActionResult> UpdateCategory(Guid CategoryId, CategoryUpdateModel categoryUpdateModel);
    }
}
