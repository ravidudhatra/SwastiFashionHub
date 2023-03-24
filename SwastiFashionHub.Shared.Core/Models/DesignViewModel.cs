using System.ComponentModel;

namespace SwastiFashionHub.Shared.Core.Models
{
    public class DesignViewModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Note { get; set; }

        [DisplayName("Design Image")]
        public string? DesignImage { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedDate { get; set; }
    }
}
