using System;
using System.Collections.Generic;

namespace WebShoppie.DataModel.Entities;

public partial class Orderproduct
{
    public long Orderproductid { get; set; }

    public long Orderid { get; set; }

    public long Productid { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
