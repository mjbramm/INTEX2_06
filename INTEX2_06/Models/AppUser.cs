using Microsoft.AspNetCore.Identity;

namespace INTEX2_06.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
