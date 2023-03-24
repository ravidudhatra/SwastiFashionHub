using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwastiFashionHub.Data.Models;

public partial class DesignImages
{
    [Key]
    public Guid Id { get; set; }
    public Guid DesignId { get; set; }
    public string DesignImage { get; set; }
}
