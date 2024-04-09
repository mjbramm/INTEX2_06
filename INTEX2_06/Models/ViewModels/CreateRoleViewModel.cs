using System.ComponentModel.DataAnnotations;

namespace INTEX2_06.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
