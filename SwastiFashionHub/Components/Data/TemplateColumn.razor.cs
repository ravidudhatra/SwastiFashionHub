using Microsoft.AspNetCore.Components;

namespace SwastiFashionHub.Components.Data
{
	public partial class TemplateColumn<TItem> : BaseColumn, IDisposable
	{
		/// <summary>
		/// The Table component reference 
		/// </summary>
		[CascadingParameter]
		protected DataTable<TItem> Context { get; set; }



		/// <summary>
		/// The contects of this templated column
		/// </summary>
		[Parameter]
		public RenderFragment<TItem> ChildContent { get; set; }



		/// <summary>
		/// Event fired on initializing component
		/// </summary>
		protected override void OnInitialized() => Context.RegisterColumn(this);



		/// <summary>
		/// Called on disposing component
		/// </summary>
		public void Dispose() => Context.UnregisterColumn(this);
	}
}
