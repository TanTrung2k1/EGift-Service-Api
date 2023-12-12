using Data.Entities;
using Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Implementations
{
    public class FeverousItemRepository : Repository<FeverousItem>, IFeverousItemRepository
    {
        public FeverousItemRepository(EgiftContext context) : base(context)
        {
        }
    }
}
