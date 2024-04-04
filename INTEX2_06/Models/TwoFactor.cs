using System.ComponentModel.DataAnnotations;
namespace INTEX2_06.Models
{
    public class TwoFactor
    {
        [Required]
        public string TwoFactorCode { get; set; }
    }
}
