using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.View
{
    public class ProductCategoryViewModel
    {
        public Guid ProductId { get; set; }

        public Guid CategoryId { get; set; }

        public string? Description { get; set; }
    }
}
