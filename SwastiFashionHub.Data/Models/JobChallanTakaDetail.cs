using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwastiFashionHub.Data.Models;

public partial class JobChallanTakaDetail
{
    [Key]
    public Guid Id { get; set; }

    public Guid JobChallanId { get; set; }

    public long FabricChallanId { get; set; }

    public string TakaDetail { get; set; }

    public virtual JobChallan JobChallan { get; set; }
}
