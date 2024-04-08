using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace INTEX2_06.Models;

public partial class Lego
{
    [Key]
    public int product_ID { get; set; }

    public string name { get; set; } = null!;

    public int year { get; set; }

    public int num_parts { get; set; }

    public string img_link { get; set; } = null!;

    public string primary_color { get; set; } = null!;

    public string secondary_color { get; set; } = null!;

    public string description { get; set; } = null!;

    public string category { get; set; } = null!;

    public int PageCount { get; set; }

    public int total_ordered { get; set; }

    public double avg_rating { get; set; }

    public double price { get; set; }
}
