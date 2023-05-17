using Microsoft.AspNetCore.Http;
using SwastiFashionHub.Common.Data.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Common.Data.Request
{
    public class PartyRequest
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int PartyType { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }
    }
}
