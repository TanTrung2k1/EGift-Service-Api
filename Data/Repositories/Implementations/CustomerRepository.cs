using Data.Entities;
using Data.Repositories.Interfaces;


namespace Data.Repositories.Implementations
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(EgiftContext context) : base(context)
        {
        }
    }
}
