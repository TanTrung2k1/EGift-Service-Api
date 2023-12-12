using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Requests.Put
{
    public class CategoryUpdateModel
    {
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
