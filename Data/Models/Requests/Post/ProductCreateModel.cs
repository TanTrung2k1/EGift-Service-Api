using Microsoft.AspNetCore.Http;
namespace Data.Models.Request
{
    public class ProductCreateModel
    {
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; } = null!;
        public List<Guid> CategoryId { get; set; } = new List<Guid>();
        public List<IFormFile> files { get; set; } = new List<IFormFile>();
    }
}
