using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Common.Data.Request
{
    public class FabricChallanRequest
    {
        public Guid Id { get; set; }

        public Guid PartyId { get; set; }

        [Required]
        public string ChallanNo { get; set; }

        public DateTime ChallanDate { get; set; }

        public Guid FabricId { get; set; }

        public int FabricType { get; set; }

        public string TakaDetail { get; set; }

        public string? ChallanImage { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
