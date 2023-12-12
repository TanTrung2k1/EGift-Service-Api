using Data.Entities;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implementations
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        private EgiftContext _context;

        public ProductCategoryRepository(EgiftContext context) : base(context)
        {
            _context = context;
        }

      
    }
}
