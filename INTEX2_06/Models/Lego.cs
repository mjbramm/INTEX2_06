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

    public int total_ordered { get; set; }

    public double avg_rating { get; set; }

    public double price { get; set; }

    public int small_rec_1 { get; set; }

    public int small_rec_2 { get; set; }

    public int small_rec_3 { get; set; }

    public int small_rec_4 { get; set; }

    public int small_rec_5 { get; set; }

    public int small_rec_6 { get; set; }

    public int small_rec_7 { get; set; }

    public int small_rec_8 { get; set; }

    public int small_rec_9 { get; set; }

    public int small_rec_10 { get; set; }

    public int pop_recommend1 { get; set; }

    public int pop_recommend2 { get; set; }

    public int pop_recommend3 { get; set; }

    public int pop_recommend4 { get; set; }

    public int pop_recommend5 { get; set; }

    public int pop_recommend6 { get; set; }

    public int pop_recommend7 { get; set; }

    public int pop_recommend8 { get; set; }

    public int pop_recommend9 { get; set; }

    public int pop_recommend10 { get; set; }

}
