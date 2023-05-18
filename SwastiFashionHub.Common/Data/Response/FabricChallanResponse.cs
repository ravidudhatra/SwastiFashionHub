using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Common.Data.Response
{
    public class FabricChallanResponse
    {
        public Guid Id { get; set; }

        public Guid PartyId { get; set; }

        [DisplayName("Challan No")]
        [Required]
        public string ChallanNo { get; set; }

        [DisplayName("Challan Date")]
        public DateTime ChallanDate { get; set; }
        
        [DisplayName("Entrty Date")]
        public DateTime CreatedDate { get; set; }
        
        public Guid FabricId { get; set; }

        public int FabricType { get; set; }

        public string TakaDetail { get; set; }

        public string? ChallanImage { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
