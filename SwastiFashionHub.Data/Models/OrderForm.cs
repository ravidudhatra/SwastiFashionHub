﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwastiFashionHub.Data.Models;

public partial class OrderForm
{
    [Key]
    public Guid Id { get; set; }

    public string JobNo { get; set; }

    public Guid PartyId { get; set; }

    public int FabricType { get; set; }

    public int DeliveryInDays { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public int DesignNo { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Unit { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? TotalQty { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? TotalMtr { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsArchived { get; set; }

    public virtual ICollection<OrderFormDesignDetail> OrderFormDesignDetails { get; set; }
}
