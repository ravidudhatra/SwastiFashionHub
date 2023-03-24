using Microsoft.AspNetCore.Components;
using SwastiFashionHub.Components.Core;
using SwastiFashionHub.Components.Data;

namespace SwastiFashionHub.Components.Layouts
{
	public partial class Form
	{
		/// <summary>
		/// Local variables
		/// </summary>
		private HashSet<IInput> inputs;



		/// <summary>
		/// Determines contents of the form
		/// </summary>
		[Parameter]
		public RenderFragment ChildContent { get; set; }



		/// <summary>
		/// Called from input components to register them in form
		/// </summary>
		/// <param name="input">The component to register</param>
		public void RegisterInput(IInput input)
		{
			if (inputs == null) inputs = new();
			inputs.Add(input);
			StateHasChanged();
		}



		/// <summary>
		/// Called from input components to unregister them from form
		/// </summary>
		/// <param name="input">The component to unregister</param>
		public void UnregisterInput(IInput input)
		{
			inputs.Remove(input);
			StateHasChanged();
		}



		/// <summary>
		/// Validates all inputs in form
		/// </summary>
		/// <returns></returns>
		public bool Validate()
		{
			foreach (var input in inputs)
			{
				input.Validate();
			}

			foreach (var input in inputs)
			{
				if (!input.Validate()){
					return false;
				}
			}

			return true;
		}
	}
}
