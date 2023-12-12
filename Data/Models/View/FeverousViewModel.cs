namespace Data.Models.View
{
    public class FeverousViewModel
    {
        public Guid Id { get; set; }
        public ICollection<FeverousItemViewModel> FeverousItems { get; set; } = new List<FeverousItemViewModel>();
    }
}
