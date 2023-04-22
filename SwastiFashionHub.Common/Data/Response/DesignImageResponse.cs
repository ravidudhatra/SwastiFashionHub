using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Common.Data.Response
{
    public class DesignImageResponse
    {
        public Guid Id { get; set; }
        public Guid DesignId { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
    }
}
