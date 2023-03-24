using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwastiFashionHub.Data.Models;

public partial class Design
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string? Note { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsArchived { get; set; }
}
