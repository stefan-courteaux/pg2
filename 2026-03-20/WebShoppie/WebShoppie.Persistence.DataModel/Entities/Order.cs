using System;
using System.Collections.Generic;

namespace WebShoppie.DataModel.Entities;

public partial class Order
{
    public long Orderid { get; set; }

    public DateTime Orderdate { get; set; }

    public long Customerid { get; set; }

    public decimal Totalprice { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Orderproduct> Orderproducts { get; set; } = new List<Orderproduct>();
}
