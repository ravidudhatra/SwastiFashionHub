using Microsoft.AspNetCore.Components;

namespace SwastiFashionHub.Components.Input
{
	public partial class SearchBox
	{
		/// <summary>
		/// The search text
		/// </summary>
		[Parameter]
		public string Value { get; set; }



		/// <summary>
		/// Event callback when user changes value in input
		/// </summary>
		[Parameter]
		public EventCallback<string> ValueChanged { get; set; }



		/// <summary>
		/// Called when user edits value in component
		/// </summary>
		/// <param name="value">The latest value in component</param>
		private void Change(string value)
		{
			Value = value;
			ValueChanged.InvokeAsync(value);
		}
	}
}
