using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class GoodsToOrder
{
    public int Id { get; set; }

    public int IdGoods { get; set; }

    public int IdOrders { get; set; }

    public int Quantity { get; set; }

    public virtual Good IdGoodsNavigation { get; set; } = null!;

    public virtual Order IdOrdersNavigation { get; set; } = null!;
}
