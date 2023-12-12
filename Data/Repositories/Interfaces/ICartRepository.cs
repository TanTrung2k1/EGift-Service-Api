using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ICartRepository:IRepository<Cart>
    {
        public Task<Cart> FirstOrDefaultAsync(Expression<Func<Cart, bool>> predicate, string include);
    }
}
