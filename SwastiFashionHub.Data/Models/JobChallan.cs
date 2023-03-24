using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwastiFashionHub.Data.Models;

public partial class JobChallan
{
    [Key]
    public Guid Id { get; set; }

    public Guid PartyId { get; set; }

    public string ChallanNo { get; set; }

    public DateTime ChallanDate { get; set; }

    public int DesignNo { get; set; }

    public Guid FabricId { get; set; }

    public string? TotalFabric { get; set; }

    public string? ChallanImage { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsArchived { get; set; }

    public virtual ICollection<JobChallanTakaDetail> JobChallanTakaDetails { get; set; }
}
