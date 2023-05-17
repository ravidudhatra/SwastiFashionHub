using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Common.Data.Response
{
    public class FabricResponse
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
    }
}
