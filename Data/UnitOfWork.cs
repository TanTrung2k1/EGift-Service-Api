using Data.Entities;
using Data.Repositories.Implementations;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EgiftContext _context;

        private IProductRepository _product = null!;
        private ICategoryRepository _category = null!;
        private ICartRepository _cart = null!;
        private ICartItemRepository _cartItem = null!;
        private IProductImageRepository _productImage = null!;
        private IFeverousItemRepository _feverousItem = null!;
        private IFeverousRepository _feverous = null!;
        private IOrderRepository _order = null!;
        private IOrderDetailRepository _orderDetail = null!;
        private ITransactionRepository _transactionRepository = null!;
        private ICustomerRepository _customer = null!;
        private IVoucherRepository _voucher = null!;

        public UnitOfWork(EgiftContext context)
        {
            _context = context;
        }

        public IProductRepository Product
        {
            get { return _product ??= new ProductRepository(_context); }
        }

        public ICategoryRepository Category
        {
            get { return _category ??= new CategoryRepository(_context); }

        }

        public ICartRepository Cart
        {
            get { return _cart ??= new CartRepository(_context); }
        }

        public ICartItemRepository CartItem
        {

            get { return _cartItem ??= new CartItemRepository(_context); }
        }


        public IProductImageRepository ProductImage
        {
            get { return _productImage ??= new ProductImageRepository(_context); }
        }

        public IFeverousItemRepository FeverousItem
        {
            get { return _feverousItem ??= new FeverousItemRepository(_context); }
        }

        public IFeverousRepository Feverous
        {
            get { return _feverous ??= new FeverousRepository(_context); }
        }

        public ICustomerRepository Customer
        {
            get { return _customer ??= new CustomerRepository(_context); }
        }

        public IOrderRepository Order
        {
            get { return _order ??= new OrderRepository(_context); }
        }

        public IOrderDetailRepository OrderDetail
        {
            get { return _orderDetail ??= new OrderDetailRepository(_context); }
        }

        public ITransactionRepository TransactionRepository
        {
            get { return _transactionRepository ??= new TransactionRepository(_context); }
        }

        public IVoucherRepository Voucher
        {
            get { return _voucher ??= new VoucherRepository(_context); }
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public IDbContextTransaction Transaction()
        {
            return _context.Database.BeginTransaction();
        }

    }
}
