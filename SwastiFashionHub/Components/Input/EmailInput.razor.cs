using Microsoft.AspNetCore.Components;
using SwastiFashionHub.Components.Core;
using SwastiFashionHub.Components.Data;
using SwastiFashionHub.Components.Layouts;

namespace SwastiFashionHub.Components.Input
{
	public partial class EmailInput : IInput, IDisposable
	{
		/// <summary>
		/// Local variables
		/// </summary>
		private string Id = Guid.NewGuid().ToString();

		private bool? isValid;
		private string errorMsg;



		/// <summary>
		/// The Form component reference 
		/// </summary>
		[CascadingParameter]
		protected Form Context { get; set; }



		/// <summary>
		/// Determines label text for input
		/// </summary>
		[Parameter]
		public string Label { get; set; }



		/// <summary>
		/// Determines if value in this component is required
		/// </summary>
		[Parameter]
		public bool Required { get; set; } = false;



		/// <summary>
		/// the value for the component
		/// </summary>
		[Parameter]
		public string Value { get; set; } = "";



		/// <summary>
		/// Event callback when user changes value in component
		/// </summary>
		[Parameter]
		public EventCallback<string> ValueChanged { get; set; }



		/// <summary>
		/// Detrmines width of the component
		/// </summary>
		[Parameter]
		public string Width { get; set; } = "100%";



		/// <summary>
		/// Event fired on initializing component
		/// </summary>
		protected override void OnInitialized() => Context.RegisterInput(this);



		/// <summary>
		/// Called on disposing component
		/// </summary>
		public void Dispose() => Context.UnregisterInput(this);



		/// <summary>
		/// Called when user edits value in component
		/// </summary>
		/// <param name="value">The latest value in component</param>
		private void Change(string value)
		{
			ValueChanged.InvokeAsync(value);
		}



		/// <summary>
		/// Validates value in component
		/// </summary>
		/// <returns>Return true or false depending upon value</returns>
		public bool Validate()
		{
			if (Required && String.IsNullOrEmpty(Value))
			{
				errorMsg = "Mandatory field";
				isValid = false;
				return false;
			}

			if(!Helper.ValidateEmailAddress(Value))
			{
				errorMsg = "Invalid email address";
				isValid = false;
				return false;
			}

			errorMsg = "";
			isValid = true;
			return true;
		}
	}
}
