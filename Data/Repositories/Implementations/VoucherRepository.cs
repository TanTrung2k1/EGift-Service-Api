using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories.Implementations
{
    public class VoucherRepository : Repository<Voucher>, IVoucherRepository
    {
        private EgiftContext _context;

        public VoucherRepository(EgiftContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Voucher> FirstOrDefaultInlude(Expression<Func<Voucher, bool>> predicate, Expression<Func<Voucher, object>> include)
        {
            return await _entities.Where(predicate).Include(include).FirstOrDefaultAsync()!;
        }

        public IQueryable<Voucher> GetWithInlude(Expression<Func<Voucher, bool>> predicate, Expression<Func<Voucher, object>> include)
        {
            return _entities.Where(predicate).Include(include);
        }
    }
}
