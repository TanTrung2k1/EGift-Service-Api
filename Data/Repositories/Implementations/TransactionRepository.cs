using Data.Entities;
using Data.Repositories.Interfaces;

namespace Data.Repositories.Implementations
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        private EgiftContext _context;

        public TransactionRepository(EgiftContext context) : base(context) 
        {
            _context = context;
        }
    }
}
