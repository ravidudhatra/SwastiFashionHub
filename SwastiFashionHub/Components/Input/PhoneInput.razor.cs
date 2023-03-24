using Microsoft.AspNetCore.Components;
using SwastiFashionHub.Components.Core;
using SwastiFashionHub.Components.Data;
using SwastiFashionHub.Components.Layouts;

namespace SwastiFashionHub.Components.Input
{
	public partial class PhoneInput : IInput, IDisposable
	{
		/// <summary>
		/// Local variables
		/// </summary>
		private string Id = Guid.NewGuid().ToString();

		private string value1 = "";
		private string value2 = "";
		private string value3 = "";

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
		public string Value
		{
			get => $"{value1}-{value2}-{value3}";
			set
			{
				string[] arr= value.Split('-');
				if (arr.Length == 3)
				{
					value1 = arr[0];
					value2 = arr[1];
					value3 = arr[2];
				}
			}
		}



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
		private void Change(string val1, string val2, string val3)
		{
			ValueChanged.InvokeAsync($"{val1}-{val2}-{val3}");
		}



		/// <summary>
		/// Validates value in component
		/// </summary>
		/// <returns>Return true or false depending upon value</returns>
		public bool Validate()
		{
			Value = $"{value1}-{value2}-{value3}";

			if (Required && String.IsNullOrEmpty(Value))
			{
				errorMsg = "Mandatory field";
				isValid = false;
				return false;
			}

			if (!Helper.ValidatePhoneNumber(Value.Replace("-", "")))
			{
				errorMsg = "Invalid phone number";
				isValid = false;
				return false;
			}
					
			errorMsg = "";
			isValid = true;
			return true;
		}
	}
}
