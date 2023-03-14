using System.Collections.Generic;

namespace SwastiFashionHub.Shared.Core.Http
{
    public class BadRequestError
    {
        public string Status { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
