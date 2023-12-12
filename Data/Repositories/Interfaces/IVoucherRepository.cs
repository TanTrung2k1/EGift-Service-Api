using Data.Entities;
using System.Linq.Expressions;

namespace Data.Repositories.Interfaces
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        IQueryable<Voucher> GetWithInlude(Expression<Func<Voucher, bool>> predicate, Expression<Func<Voucher, object>> include);
        Task<Voucher> FirstOrDefaultInlude(Expression<Func<Voucher, bool>> predicate, Expression<Func<Voucher, object>> include);
    }
}
