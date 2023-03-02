using System;
using System.Collections.Generic;

namespace SwastiFashionHub.Data.Models;

public partial class OrderForm
{
    public long Id { get; set; }

    public string JobNo { get; set; } = null!;

    public int PartyId { get; set; }

    public int FabricType { get; set; }

    public int DeliveryInDays { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public int DesignNo { get; set; }

    public decimal Unit { get; set; }

    public decimal? TotalQty { get; set; }

    public decimal? TotalMtr { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsArchived { get; set; }

    public virtual ICollection<OrderFormDesignDetail> OrderFormDesignDetails { get; } = new List<OrderFormDesignDetail>();
}
