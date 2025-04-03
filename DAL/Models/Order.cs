using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Order
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public int IdSuppliers { get; set; }



    public virtual ICollection<GoodsToOrder> GoodsToOrders { get; set; } = new List<GoodsToOrder>();

    public virtual Supplier IdSuppliersNavigation { get; set; } = null!;
}
