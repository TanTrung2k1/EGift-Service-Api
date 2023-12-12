namespace Data.Models.Filters
{
    public class ProductFilterModel
    {
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
        public bool? According { get; set;}
    }
}
