using Microsoft.AspNetCore.Components;

namespace SwastiFashionHub.Components.Display
{
	public partial class InputError
	{
		/// <summary>
		/// Determines error message to be displayed
		/// </summary>
		[Parameter]
		public string Message { get; set; }



		/// <summary>
		/// Event callback while clicking on component
		/// </summary>
		[Parameter]
		public EventCallback OnClick { get; set; }
	}
}
