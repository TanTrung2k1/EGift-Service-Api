using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Models.Requests.Post;
using Data.Models.Requests.Put;
using Data.Models.View;
using Data.Repositories.Interfaces;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System.Security.Cryptography;
using Utility.Enums;

namespace Service.Implementations
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IFeverousRepository _feverousRepository;
        private readonly ICartRepository _cartRepository;
        private readonly ISendMailService _sendMailService;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper, ISendMailService sendMailService) : base(unitOfWork, mapper)
        {
            _customerRepository = unitOfWork.Customer;
            _feverousRepository = unitOfWork.Feverous;
            _cartRepository = unitOfWork.Cart;
            _sendMailService = sendMailService;
        }

        public async Task<CustomerViewModel> GetCustomer(Guid id)
        {
            return await _customerRepository.GetMany(customer => customer.Id.Equals(id))
                .ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider).FirstOrDefaultAsync() ?? null!;
        }

        private UserStatus? StatusOfUser(bool? isActive)
        {
            UserStatus? status = null;
            if (isActive.HasValue)
            {
                if (isActive.Value)
                {
                    status = UserStatus.Activated;
                }
                else
                {
                    status = UserStatus.DeActivated;
                }
            }
            return status;
        }

        public async Task<IActionResult> RegisterCustomer(RegisterRequest request)
        {
            if (_customerRepository.Any(c => c.Username.Equals(request.Username) || c.Email.Equals(request.Email)))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Password = request.Password,
                Name = request.Name,
                Email = request.Email,
                Phone = "",
                Address = "",
                VerifyToken = CreateRandomToken(),
                IsActive = false,
                CreateAt = DateTime.Now
            };
            _customerRepository.Add(customer);

            var feverous = new Feverous
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
            };
            _feverousRepository.Add(feverous);

            //var cart = new Cart
            //{
                //Id = Guid.NewGuid(),
                //CustomerId = customer.Id
            //};
            //_cartRepository.Add(cart);



            await _unitOfWork.SaveChanges();

            await _sendMailService.SendVerificationEmail(customer.Email, customer.VerifyToken);

            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        public async Task<IActionResult> AddAvatar(Guid id, IFormFile file)
        {
            var customer = await _customerRepository.GetMany(customer => customer.Id.Equals(id)).FirstOrDefaultAsync();
            if (customer != null)
            {
                customer.AvatarUrl = await UploadImageToFirebaseStorage(file);
                _customerRepository.Update(customer);
                var result = await _unitOfWork.SaveChanges();
                if (result > 0)
                {
                    return new StatusCodeResult(StatusCodes.Status201Created);
                }
            }
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }

        private async Task<string> UploadImageToFirebaseStorage(IFormFile file)
        {
            var storage = new FirebaseStorage("e-gift-6276a.appspot.com");
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var imageUrl = await storage.Child("images")
                                        .Child(imageName)
                                        .PutAsync(file.OpenReadStream());
            return imageUrl;
        }

        public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomerRequest request)
        {
            var existingCustomer = await _customerRepository.GetMany(customer => customer.Id.Equals(id)).FirstOrDefaultAsync();
            if (existingCustomer == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            }

            existingCustomer.Name = request.Name ?? existingCustomer.Name;
            existingCustomer.Phone = request.Phone ?? existingCustomer.Phone;
            existingCustomer.Address = request.Address ?? existingCustomer.Address;

            _customerRepository.Update(existingCustomer);
            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            return new StatusCodeResult(StatusCodes.Status400BadRequest);

        }



        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var customer = await _customerRepository.GetMany(customer => customer.Id.Equals(id)).FirstOrDefaultAsync();
            if (customer == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            customer.IsActive = false;
            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return new StatusCodeResult(StatusCodes.Status204NoContent);
            }
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }

        public async Task<IActionResult> UpdateCustomerPassword(Guid id, UpdatePasswordRequest request)
        {
            var customer = await _customerRepository.GetMany(customer => customer.Id.Equals(id)).FirstOrDefaultAsync();

            if (customer == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            if (!customer.Password.Equals(request.OldPassword))
            {
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }

            customer.Password = request.NewPassword;

            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return new StatusCodeResult(StatusCodes.Status201Created);
            }
            return new StatusCodeResult(StatusCodes.Status400BadRequest);

        }

        public async Task<IActionResult> VerifyCustomer(string token)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(c => c.VerifyToken.Equals(token));
            if (customer == null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);

            }
            customer.VerifyTime = DateTime.Now;
            customer.IsActive = true;


            var result = await _unitOfWork.SaveChanges();
            if (result > 0)
            {
                return new StatusCodeResult(StatusCodes.Status200OK);
            }
            return new StatusCodeResult(StatusCodes.Status400BadRequest);

        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
