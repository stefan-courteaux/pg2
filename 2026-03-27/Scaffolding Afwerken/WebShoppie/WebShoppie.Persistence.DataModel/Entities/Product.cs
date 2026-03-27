using System;
using System.Collections.Generic;

namespace WebShoppie.DataModel.Entities;

public partial class Product
{
    public long Productid { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stockcount { get; set; }

    public virtual ICollection<Orderproduct> Orderproducts { get; set; } = new List<Orderproduct>();
}
