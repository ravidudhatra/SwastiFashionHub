using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Common.Data.Request
{
    public class DesignRequest
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Note { get; set; }

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }
        
        public List<DesignImagesRequest>? DesignImages { get; set; }
    }
}
