using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace INTEX2_06.Models.ViewModels
{
    public class CreateOrderViewModel
    {
        [Key]
        [Required]
        public int transaction_ID { get; set; }

        public string? date { get; set; }

        public string? day_of_week { get; set; }

        public int? time { get; set; }

        public string? entry_mode { get; set; }

        public double? amount { get; set; }

        public string? type_of_transaction { get; set; }

        public string? country_of_transaction { get; set; }

        public string? shipping_address { get; set; }

        public string? bank { get; set; }

        public string? type_of_card { get; set; }

        public int? fraud { get; set; }

        public int? predict_fraud { get; set; }

        public int? complete { get; set; }
        [ForeignKey("AppUser")]
        public string? UserID { get; set; }
    }
}
