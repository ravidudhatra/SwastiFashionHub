using Microsoft.AspNetCore.Components;

using SwastiFashionHub.Components.Core;
using static SwastiFashionHub.Components.Core.Enums;

namespace SwastiFashionHub.Components.Action
{
	public partial class Button
	{
		/// <summary>
		/// Determines icon for the button
		/// </summary>
		[Parameter]
		public string Icon { get; set; }



		/// <summary>
		/// Event callback when user clicks this button
		/// </summary>
		[Parameter]
		public EventCallback OnClick { get; set; }



		/// <summary>
		/// Determines text of the button
		/// </summary>
		[Parameter]
		public string Text { get; set; }



		/// <summary>
		/// Determines type of button
		/// </summary>
		[Parameter]
		public ButtonType Type { get; set; } = ButtonType.Primary;
	}
}
