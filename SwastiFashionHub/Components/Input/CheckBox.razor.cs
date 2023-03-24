using Microsoft.AspNetCore.Components;

namespace SwastiFashionHub.Components.Input
{
	public partial class CheckBox
	{
		/// <summary>
		/// Local variables
		/// </summary>
		private string Id = Guid.NewGuid().ToString();



		/// <summary>
		/// Determines label text for input
		/// </summary>
		[Parameter]
		public string Label { get; set; }



		/// <summary>
		/// The value of the component
		/// </summary>
		[Parameter]
		public bool Value { get; set; } = false;



		/// <summary>
		/// Event callback on value change
		/// </summary>
		[Parameter]
		public EventCallback<bool> ValueChanged { get; set; }
	}
}
