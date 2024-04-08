namespace INTEX2_06.Models.ViewModels
{
    public class LegosListViewModel
    {
        public IEnumerable<Lego> Legos { get; set; }

        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();

        public string? CurrentLegoCategory { get; set; }
    }
}
