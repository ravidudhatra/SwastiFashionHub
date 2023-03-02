using System;
using System.Collections.Generic;

namespace SwastiFashionHub.Data.Models;

public partial class JobChallanTakaDetail
{
    public long Id { get; set; }

    public long JobChallanId { get; set; }

    public long FabricChallanId { get; set; }

    public string TakaDetail { get; set; } = null!;

    public virtual JobChallan JobChallan { get; set; } = null!;
}
