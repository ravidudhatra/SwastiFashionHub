using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SwastiFashionHub.Data.Models;

public partial class DesignImage
{
    [Key]
    public Guid Id { get; set; }
    public Guid DesignId { get; set; }
    public string ImageUrl { get; set; }
}
