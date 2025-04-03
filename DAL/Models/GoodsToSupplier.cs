using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class GoodsToSupplier
{
    public int Id { get; set; }

    public int IdSuppliers { get; set; }

    public int IdGoods { get; set; }

    public virtual Good IdGoodsNavigation { get; set; } = null!;

    public virtual Supplier IdSuppliersNavigation { get; set; } = null!;
}
