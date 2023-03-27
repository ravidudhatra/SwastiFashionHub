using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Common.Data.Response
{
    public class DesignResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Note { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public List<DesignImageResponse> DesignImages { get; set; }
    }
}
