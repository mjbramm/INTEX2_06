using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace INTEX2_06.Models;

public partial class Customer
{
    [Key]
    public int customer_ID { get; set; }

    public string first_name { get; set; } = null!;

    public string last_name { get; set; } = null!;

    public string birth_date { get; set; } = null!;

    public string counrty_of_residence { get; set; } = null!; //change to country later when we push the data again

    public string gender { get; set; } = null!;

    public double age { get; set; }
}
