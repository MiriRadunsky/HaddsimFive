using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string Company { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string RepresentativeName { get; set; } = null!;

    public virtual ICollection<GoodsToSupplier> GoodsToSuppliers { get; set; } = new List<GoodsToSupplier>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
