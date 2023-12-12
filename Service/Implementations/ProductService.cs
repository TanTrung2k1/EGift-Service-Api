using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data;
using Data.Entities;
using Data.Models.Filters;
using Data.Models.Request;
using Data.Models.View;
using Data.Repositories.Interfaces;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Utility.Enums;

namespace Service.Implementations
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _productRepository = unitOfWork.Product;
            _productImageRepository = unitOfWork.ProductImage;
        }

        public async Task<IActionResult> GetProducts(ProductFilterModel filter)
        {
            var query = _productRepository.GetAll();
            if (filter.Name != null)
            {
                query = query.Where(product => product.Name.Contains(filter.Name));
            }
            if (filter.CategoryId != null)
            {
                query = _productRepository.GetMany(x => x.Categories.Any(x => x.Id.Equals(filter.CategoryId)));
            }
            if (filter.CategoryName != null)
            {
                query = query.Where(product => product.Categories.Any(category => category.Name.Contains(filter.CategoryName)));
            }
            if (filter.MaxPrice != null && filter.MinPrice != null)
            {
                query = query.Where(x => x.Price >= filter.MinPrice && x.Price <= filter.MaxPrice);
            }
            if (filter.According != null && filter.According == true)
            {
                query = query.OrderBy(x => x.Price);
            }
            if (filter.According != null && filter.According == false)
            {
                query = query.OrderByDescending(x => x.Price);
            }
            else if (filter.MinPrice != null && filter.MaxPrice == null)
            {
                query = query.Where(x => x.Price >= filter.MinPrice);
            }
            else if (filter.MinPrice == null && filter.MaxPrice != null)
            {
                query = query.Where(x => x.Price <= filter.MaxPrice);
            }
            var products = await query
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return new JsonResult(products);
        }

        public async Task<IActionResult> GetProduct(Guid id)
        {
            var product = await _productRepository.GetMany(product => product.Id.Equals(id))
                .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            if (product != null)
            {
                return new JsonResult(product);
            }
            return new StatusCodeResult(StatusCodes.Status404NotFound);
        }

        public async Task<IActionResult> GetProductByCategoryId(Guid idCategory)
        {
            var products = await _productRepository.GetMany(x => x.Categories.Any(c => c.Id.Equals(idCategory))).ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider).ToListAsync();
            return new JsonResult(products);
        }

        public async Task<IActionResult> CreateProduct(ProductCreateModel productCreateModel)
        {
            if (productCreateModel.files == null || productCreateModel.files.Count != 4)
            {
                return new StatusCodeResult(400);
            }
            var categories = await _unitOfWork.Category.GetMany(x => productCreateModel.CategoryId.Contains(x.Id)).ToListAsync();
            if (categories != null)
            {
                Product product = new Product
                {
                    Id = Guid.NewGuid(),
                    Name = productCreateModel.Name,
                    Description = productCreateModel.Description,
                    Price = productCreateModel.Price,
                    Quantity = productCreateModel.Quantity,
                    Status = ProductStatus.Active,
                    Categories = categories,
                };
                _productRepository.Add(product);

                //--upload file
                foreach (var file in productCreateModel.files)
                {
                    var imageProduct = new ProductImage
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        Url = await UploadProductImageToFirebase(file)
                    };
                    _productImageRepository.Add(imageProduct);
                }

                await _unitOfWork.SaveChanges();
                return await GetProduct(product.Id);
            }
            return new StatusCodeResult(400);
        }

        private async Task<string?> UploadProductImageToFirebase(IFormFile file)
        {
            var storage = new FirebaseStorage("e-gift-6276a.appspot.com");
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var imageUrl = await storage.Child("images")
                                        .Child(imageName)
                                        .PutAsync(file.OpenReadStream());
            return imageUrl;
        }

        public async Task<IActionResult> UpdateProduct(Guid ProductId, ProductUpdateModel productUpdateModel)
        {
            var product = await _productRepository.GetMany(x => x.Id.Equals(ProductId)).Include(x => x.Categories).Include(x => x.ProductImages).FirstOrDefaultAsync();
            if (product != null)
            {
                product.Name = productUpdateModel.Name ?? product.Name;
                product.Quantity = productUpdateModel.Quantity ?? product.Quantity;
                
                if(product.Quantity > 0)
                {
                    product.Status = ProductStatus.Active;
                }
                else if(product.Quantity <= 0)
                {
                    product.Status = ProductStatus.RunOut;
                }

                product.Price = productUpdateModel.Price ?? product.Price;
                product.Description = productUpdateModel.Description ?? product.Description;

                if (productUpdateModel.CategoryId != null && productUpdateModel.CategoryId.Count > 0)
                {
                    var categories = await _unitOfWork.Category.GetMany(x => productUpdateModel.CategoryId.Contains(x.Id)).ToListAsync();
                    product.Categories.Clear();
                    product.Categories = categories;
                }

                if (productUpdateModel.files != null && productUpdateModel.files.Count != 0)
                {
                    if (productUpdateModel.files.Count != 4)
                    {
                        return new StatusCodeResult(400);
                    }
                    var listImage = await _productImageRepository.GetMany(image => image.ProductId.Equals(ProductId)).ToListAsync();
                    foreach (var image in listImage)
                    {
                        _productImageRepository.Remove(image);
                    }
                    foreach (var file in productUpdateModel.files)
                    {
                        var imageProduct = new ProductImage
                        {
                            Id = Guid.NewGuid(),
                            ProductId = product.Id,
                            Url = await UploadProductImageToFirebase(file)
                        };
                        _productImageRepository.Add(imageProduct);
                    }
                }

                product.UpdateAt = DateTime.Now;
                _unitOfWork.Product.Update(product);
            }
            return await _unitOfWork.SaveChanges() > 0 ? await GetProduct(ProductId) : new StatusCodeResult(500);
        }

        public async Task<IActionResult> RemoveProduct(Guid idProduct)
        {
            var product = await _productRepository.FirstOrDefaultAsync(x => x.Id.Equals(idProduct));
            if (product != null)
            {
                product.Status = "Inactive";
                return await _unitOfWork.SaveChanges() > 0 ? new JsonResult(product) : new JsonResult(new StatusCodeResult(500));
            }
            return new JsonResult(null);
        }

        public async Task<IActionResult> GetProductImage(Guid idProduct)
        {
            var product = await _unitOfWork.Product.FirstOrDefaultAsync(x => x.Id.Equals(idProduct));
            if (product != null)
            {
                var productImg = _unitOfWork.ProductImage.GetMany(x => x.ProductId.Equals(idProduct));
                return new JsonResult(productImg);
            }
            return new StatusCodeResult(404);
        }


    }
}
