using Data;
using Data.Entities;
using Data.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Data.Models.Filters;
using Data.Models.View;
using Data.Models.Requests.Put;
using AutoMapper;
using Data.Repositories.Interfaces;
using AutoMapper.QueryableExtensions;

namespace Service.Implementations
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoriesRepository;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _categoriesRepository = _unitOfWork.Category;
        }

        public async Task<IActionResult> CreateCategory(CategoryCreateModel categoryCreateModel)
        {
            Category category = new Category {
            Id=Guid.NewGuid(),
            Name=categoryCreateModel.Name,  
            Description=categoryCreateModel.Description,
            CreateAt=DateTime.Now,
            };
            _categoriesRepository.Add(category);
            return await _unitOfWork.SaveChanges() > 0 ? new JsonResult(category) : new StatusCodeResult(500);
        }

        public async Task<IActionResult> GetCategory(CategoryFilterModel categoryFilter)
        {
            var query = _categoriesRepository.GetAll();
            if (categoryFilter.Name != null)
            {
                query = query.Where(x => x.Name.Contains(categoryFilter.Name));
            }
            if (categoryFilter.Description != null)
            {
                query = query.Where(x => x.Description.Contains(categoryFilter.Description));
            }
            var rs = await query.ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider).ToListAsync();
            return  new JsonResult(rs);
        }

        public async Task<IActionResult> GetCategoryById(Guid id)
        {
           var query =await _categoriesRepository.GetMany(x=>x.Id.Equals(id)).ProjectTo<CategoryViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            return query != null ? new JsonResult(query) : new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        public async Task<IActionResult> UpdateCategory(Guid CategoryId, CategoryUpdateModel categoryUpdateModel)
        {
            var categories = await _categoriesRepository.FirstOrDefaultAsync(x => x.Id.Equals(CategoryId));
            if (categories != null)
            {
                categories.Name = categoryUpdateModel.Name ?? categories.Name;
                categories.Description = categoryUpdateModel.Description ?? categories.Description;
                await _unitOfWork.SaveChanges();
            }
            return categories is not null ? await GetCategoryById(CategoryId) : new StatusCodeResult(400);
        }
    }
}
