using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
//using SwastiFashionHub.Repositories;

namespace SwastiFashionHub.Components.Display
{
	public partial class AppHeader
	{
		/// <summary>
		/// Authentication state provider
		/// </summary>
		[CascadingParameter]
		private Task<AuthenticationState> AuthenticationState { get; set; }



		///// <summary>
		///// Injecting authentication service
		///// </summary>
		//[Inject]
		//public AuthStateService AuthStateService { get; set; }



		///// <summary>
		///// Injecting navigtion manager
		///// </summary>
		//[Inject]
		//public NavigationManager NavigationManager { get; set; }



		///// <summary>
		///// Injecting JS runtime
		///// </summary>
		//[Inject]
		//public IJSRuntime JS { get; set; }	



		///// <summary>
		///// The speciality of user eg. eye specialist etc.
		///// </summary>
		//[Parameter]
		//public string UserSpeciality { get; set; }



		///// <summary>
		///// The name of loggedin user
		///// </summary>
		//[Parameter]
		//public string UserName { get; set; }



		///// <summary>
		///// Navigates to given url
		///// </summary>
		///// <param name="url"></param>
		//private void NavigateTo(string url) => NavigationManager.NavigateTo(url);



		///// <summary>
		///// Event fired on initializing component
		///// </summary>
		///// <returns></returns>
		//protected override async Task OnAfterRenderAsync(bool firstRender)
		//{
		//	var state = await AuthenticationState;

		//	if (state is not null)
		//	{
		//		UserName = state.User.Identities.FirstOrDefault().Claims.FirstOrDefault(c => c.Type == "FullName").Value.ToString();
		//	}

		//	StateHasChanged();
		//}



		///// <summary>
		///// Logsout user and navigates to login page
		///// </summary>
		///// <returns></returns>
		//private async Task Logout()
		//{
		//	bool confirm = await  JS.InvokeAsync<bool>("confirm", "Do you really want to logout?");

		//	if (confirm)
		//	{
		//		NavigationManager.NavigateTo("/");
		//		await AuthStateService.Logout();
		//	}
		//}
	}
}
