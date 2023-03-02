using System;
using System.Collections.Generic;

namespace SwastiFashionHub.Data.Models;

public partial class Fabric
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public bool IsArchived { get; set; }
}
