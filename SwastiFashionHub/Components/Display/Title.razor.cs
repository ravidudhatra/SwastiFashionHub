using Microsoft.AspNetCore.Components;

namespace SwastiFashionHub.Components.Display
{
	public partial class Title
	{
		/// <summary>
		/// Determines if back navigation link is shown or not
		/// </summary>
		[Parameter]
		public bool ShowBackNavigation{ get; set; } = false;



		/// <summary>
		/// Determines text of the title
		/// </summary>
		[Parameter]
		public string Text { get; set; }
	}
}
