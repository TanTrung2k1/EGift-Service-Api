using Data.Entities;
using Data.Repositories.Interfaces;
using Data.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

public class CartRepository : Repository<Cart>, ICartRepository
{
    private EgiftContext _context;

    public CartRepository(EgiftContext context) : base(context)
    {
        _context = context;
    }
    public async Task<Cart> FirstOrDefaultAsync(Expression<Func<Cart, bool>> predicate, string include)
    {

        return await _entities.Include(include).FirstOrDefaultAsync(predicate) ?? null!;
    }
}