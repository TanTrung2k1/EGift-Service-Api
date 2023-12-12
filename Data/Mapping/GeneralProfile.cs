using AutoMapper;
using Data.Entities;
using Data.Models.View;

namespace Data.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<ProductImage, ProductImageViewModel>();
            CreateMap<CartItem, CartItemViewModel>();
            CreateMap<Cart, CartViewModel>();
            CreateMap<Order, OrderViewModel>().ForMember(vm => vm.Discount, opt => opt.MapFrom(x => x.Voucher! != null ? x.Voucher.Discount : 0));
            CreateMap<OrderDetail, OrderDetailViewModel>();
            CreateMap<Customer, CustomerViewModel>();
            CreateMap<Product, CartProductViewModel>()
                .ForMember(vm => vm.ProductImage, otp => otp.MapFrom(product => product.ProductImages.FirstOrDefault()));

            CreateMap<Feverous, FeverousViewModel>();
            CreateMap<FeverousItem, FeverousItemViewModel>();
            CreateMap<Product, FeverousProductViewModel>()
                .ForMember(vm => vm.ProductImage, otp => otp.MapFrom(product => product.ProductImages.FirstOrDefault()));

            CreateMap<Customer, CustomerOrderViewModel>();
            CreateMap<Voucher, VoucherViewModel>().ForMember(vm => vm.StartDate, opt => opt.MapFrom(x => x.StartDate.ToString("MM/dd/yyyy HH:mm:ss"))).ForMember(vm => vm.EndDate, opt => opt.MapFrom(x => x.EndDate.ToString("MM/dd/yyyy HH:mm:ss")));
        }
    }
}
