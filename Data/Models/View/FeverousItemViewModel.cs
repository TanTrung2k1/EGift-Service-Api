using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.View
{
    public class FeverousItemViewModel
    {
        public Guid Id { get; set; }

        public FeverousProductViewModel Product { get; set; } = null!;
    }
}
