using System.ComponentModel.DataAnnotations;

namespace INTEX2_06.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}