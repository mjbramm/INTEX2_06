using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace INTEX2_06.Models;

public partial class LineItem
{
    [Key]
    public int transaction_ID { get; set; }

    public int product_ID { get; set; }

    public int qty { get; set; }

    public int rating { get; set; }


}
