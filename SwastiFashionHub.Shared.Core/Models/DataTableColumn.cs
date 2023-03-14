using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core.Models
{
    public class DataTableColumn
    {
        public string Header { get; set; }
        public Func<object, object> GetValue { get; set; }
    }
}
