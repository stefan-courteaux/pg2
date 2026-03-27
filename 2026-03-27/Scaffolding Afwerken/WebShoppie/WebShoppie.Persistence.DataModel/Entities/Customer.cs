using System;
using System.Collections.Generic;

namespace WebShoppie.DataModel.Entities;

public partial class Customer
{
    public long Customerid { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public DateTime Dateofbirth { get; set; }

    public string Email { get; set; } = null!;

    public string Addressline1 { get; set; } = null!;

    public string Addressline2 { get; set; } = null!;

    public string? Addressline3 { get; set; }

    public string Country { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
