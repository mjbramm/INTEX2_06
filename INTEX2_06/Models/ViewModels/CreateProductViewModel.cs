using System.ComponentModel.DataAnnotations;

namespace INTEX2_06.Models.ViewModels
{
    public class CreateProductViewModel
    {
        [Key]
        [Required]
        public int product_ID { get; set; }
        [Required]
        public string name { get; set; } = null!;
        [Required]
        public int year { get; set; }
        [Required]
        public int num_parts { get; set; }
        [Required]
        public string img_link { get; set; } = null!;
        [Required]
        public string primary_color { get; set; } = null!;
        [Required]
        public string secondary_color { get; set; } = null!;
        [Required]
        public string description { get; set; } = null!;
        [Required]
        public string category { get; set; } = null!;
        [Required]
        public double price { get; set; }
    }
}
