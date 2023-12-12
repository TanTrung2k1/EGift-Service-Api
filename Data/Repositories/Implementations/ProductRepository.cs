using Data.Entities;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Implementations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private EgiftContext _context;

        public ProductRepository(EgiftContext context) : base(context)
        {
            _context = context;
        }

     
    }
}
