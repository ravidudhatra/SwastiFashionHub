using System;
using System.Collections.Generic;

namespace SwastiFashionHub.Data.Models;

public partial class Design
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Note { get; set; }

    public string? DesignImage { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsArchived { get; set; }
}
