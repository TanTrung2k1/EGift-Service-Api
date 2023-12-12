using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data
{
    public interface IUnitOfWork
    {
        public IProductRepository Product { get; }
        public ICategoryRepository Category { get; }
        public ICartRepository Cart { get; }
        public ICartItemRepository CartItem { get; }
        public IProductImageRepository ProductImage { get; }
        public IFeverousItemRepository FeverousItem { get; }
        public IFeverousRepository Feverous { get; }
        public ICustomerRepository Customer { get; }
        public IOrderRepository Order { get; }
        public IOrderDetailRepository OrderDetail { get; }
        public ITransactionRepository TransactionRepository { get; }
        public IVoucherRepository Voucher { get; }
        Task<int> SaveChanges();
        IDbContextTransaction Transaction();
    }
}
