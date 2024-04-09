using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INTEX2_06.Models;

public partial class Lego
{
    [Key]
    public int customer_ID { get; set; }
    [Key]
    public int Id { get; set; }
}
