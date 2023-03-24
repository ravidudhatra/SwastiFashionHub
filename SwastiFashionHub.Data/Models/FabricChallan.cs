using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwastiFashionHub.Data.Models;

public partial class FabricChallan
{
    [Key]
    public Guid Id { get; set; }

    public Guid PartyId { get; set; }

    [Required]
    public string ChallanNo { get; set; }

    public DateTime ChallanDate { get; set; }

    public Guid FabricId { get; set; }

    public int FabricType { get; set; }

    public string TakaDetail { get; set; }

    public string? ChallanImage { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsArchived { get; set; }
}
