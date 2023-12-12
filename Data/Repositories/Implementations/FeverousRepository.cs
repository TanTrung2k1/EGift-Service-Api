using Data.Entities;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implementations
{
    public class FeverousRepository : Repository<Feverous>, IFeverousRepository
    {
        public FeverousRepository(EgiftContext context) : base(context)
        {
        }
    }
}
