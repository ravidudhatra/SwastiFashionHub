using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core.Models
{
    public class DesignViewModel
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Note { get; set; }

        public string? DesignImage { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
