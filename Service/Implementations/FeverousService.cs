using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Models.View;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Service.Implementations
{
    public class FeverousService : BaseService, IFeverousService
    {
        private IFeverousRepository _feverousRepository;
        private IFeverousItemRepository _feverousItemRepository;

        public FeverousService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _feverousRepository = unitOfWork.Feverous;
            _feverousItemRepository = unitOfWork.FeverousItem;
        }

        public async Task<IActionResult> GetFeverousByCustomerID(Guid customerID)
        {
            var feverous = await _feverousRepository.GetMany(feverous => feverous.CustomerId.Equals(customerID))
                .ProjectTo<FeverousViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (feverous != null)
            {
                return new JsonResult(feverous);
            }
            return null!;
        }

        public async Task<IActionResult> AddFeverous(Guid customerID, Guid productID)
        {
            var feverous = await _feverousRepository.GetMany(feverous => feverous.CustomerId.Equals(customerID)).FirstOrDefaultAsync();
            if (feverous != null)
            {
                // return if product is exist
                if (!_feverousItemRepository.Any(item => item.ProductId.Equals(productID)))
                {

                    var feverousItem = new FeverousItem
                    {
                        Id = Guid.NewGuid(),
                        FeverousId = feverous.Id,
                        ProductId = productID
                    };

                    _feverousItemRepository.Add(feverousItem);
                    var result = await _unitOfWork.SaveChanges();
                    if (result > 0)
                    {
                        return await GetFeverousByCustomerID(customerID);
                    }
                }

            }
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }

        public async Task<IActionResult> DeleteFeverousItem(Guid customerId, Guid feverousItemID)
        {
            var feverousItem = await _feverousItemRepository
                .GetMany(item => item.Id.Equals(feverousItemID))
                .FirstOrDefaultAsync();
            if (feverousItem != null)
            {
                _feverousItemRepository.Remove(feverousItem);

                var result = await _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    return await GetFeverousByCustomerID(customerId);
                }
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }
    }
}
