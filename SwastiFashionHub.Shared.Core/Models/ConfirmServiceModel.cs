using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core.Models
{
    public class ConfirmServiceModel
    {
        //public string Id { get; set; }
        public string Message { get; set; }
        public string DiscardButtonName { get; set; }
        public string SaveButtonName { get; set; }
        public Action DiscardButton { get; set; }
        public Action SaveButton { get; set; }
    }
}
