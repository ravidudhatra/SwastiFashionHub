using System;
using System.Collections.Generic;

namespace SwastiFashionHub.Data.Models;

public partial class FabricChallan
{
    public long Id { get; set; }

    public int PartyId { get; set; }

    public string ChallanNo { get; set; } = null!;

    public DateTime ChallanDate { get; set; }

    public int FabricId { get; set; }

    public int FabricType { get; set; }

    public string TakaDetail { get; set; } = null!;

    public string? ChallanImage { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsArchived { get; set; }
}
