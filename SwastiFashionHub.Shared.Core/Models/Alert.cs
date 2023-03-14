using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core.Models
{
    public class Alert
    {
        public string Id { get; set; }
        public AlertType Type { get; set; }
        public string Message { get; set; }
        public bool AutoClose { get; set; }
        public bool KeepAfterRouteChange { get; set; }
        public bool Fade { get; set; }
        public string NavigationURL { get; set; }
        public string NavigationMessage { get; set; }
    }

    public enum AlertType
    {
        Success,
        Error,
        Info,
        Warning,
        Primary,
        Secondary,
        Light,
        Dark
    }
}
