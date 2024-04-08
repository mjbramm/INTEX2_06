using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace INTEX2_06.Models;

public partial class Order
{
    [Key]
    public int transaction_ID { get; set; }

    public int customer_ID { get; set; }

    public string date { get; set; }

    public string day_of_week { get; set; }

    public string time { get; set; }

    public string entry_mode { get; set; }

    public int amount { get; set; }

    public string type_of_transaction { get; set; }

    public string country_of_transaction { get; set; }

    public string shipping_address { get; set; }

    public string bank { get; set; }

    public string type_of_card { get; set; }

    public bool fraud { get; set; }

    public bool predict_fraud { get; set; }

    public bool complete { get; set; }
}
