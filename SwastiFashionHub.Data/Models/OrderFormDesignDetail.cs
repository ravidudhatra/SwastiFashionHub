using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SwastiFashionHub.Data.Models;

public partial class OrderFormDesignDetail
{
    [Key]
    public Guid Id { get; set; }

    public Guid OrderFormId { get; set; }

    public string ColorCode { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? ColorQty { get; set; }

    public virtual OrderForm OrderForm { get; set; }
}
