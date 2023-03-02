using System;
using System.Collections.Generic;

namespace SwastiFashionHub.Data.Models;

public partial class OrderFormDesignDetail
{
    public long Id { get; set; }

    public long OrderFormId { get; set; }

    public string ColorCode { get; set; } = null!;

    public decimal? ColorQty { get; set; }

    public virtual OrderForm OrderForm { get; set; } = null!;
}
