using Microsoft.AspNetCore.Components;

namespace SwastiFashionHub.Components.Layouts
{
	public partial class Card
	{
		/// <summary>
		/// Determines card body
		/// </summary>
		[Parameter]
		public RenderFragment ChildContent { get; set; }



		/// <summary>
		/// Detrmines text to be displayed in card header
		/// </summary>
		[Parameter]
		public string Heading { get; set; }
	}
}
