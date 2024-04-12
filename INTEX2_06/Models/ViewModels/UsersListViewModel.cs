namespace INTEX2_06.Models.ViewModels
{
    public class UsersListViewModel
    {
        public IEnumerable<AppUser> Users { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}
