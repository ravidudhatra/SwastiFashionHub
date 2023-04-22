
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using SwastiFashionHub.Shared.Core.Models;
using SwastiFashionHub.Shared.Core.Services.Interface;

namespace SwastiFashionHub.Shared
{
    public partial class Confirm : IDisposable
    {
        [Inject]
        private IConfirmService ConfirmService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private ConfirmServiceModel ConfirmServiceModel = null;

        private bool ShowModel { get; set; } = false;

        protected override void OnInitialized()
        {
            this.ConfirmServiceModel = null;
            // subscribe to new unsaved and location change events
            ConfirmService.OnConfirmChange += OnConfirmChange;
            NavigationManager.LocationChanged += OnLocationChange;
        }

        /// <summary>
        /// Dispose unused objects to release memory
        /// </summary>
        public void Dispose()
        {
            // unsubscribe from unsaved and location change events
            this.ConfirmServiceModel = null;
            ConfirmService.OnConfirmChange -= OnConfirmChange;
            NavigationManager.LocationChanged -= OnLocationChange;
        }

        /// <summary>
        /// Invoke when user process to open confirm dialogue
        /// </summary>
        /// <param name="confirmServiceModel"></param>
        private async void OnConfirmChange(ConfirmServiceModel confirmServiceModel)
        {
            // clear unsaved when an empty is received
            if (confirmServiceModel == null || (string.IsNullOrWhiteSpace(confirmServiceModel.Message) ||
               string.IsNullOrWhiteSpace(confirmServiceModel.SaveButtonName)))
            {
                this.ConfirmServiceModel = null;
                ShowModel = false;
            }
            else
            {
                this.ConfirmServiceModel = confirmServiceModel;
                ShowModel = true;
            }
            await InvokeAsync(() => { StateHasChanged(); });
        }

        /// <summary>
        /// To clear confirm dialogue when location changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnLocationChange(object sender, LocationChangedEventArgs e)
        {
            this.ConfirmServiceModel = null;
            await ConfirmService.Clear();
        }

        private void CloseModelClick()
        {
            ShowModel = false;
            StateHasChanged();
        }
    }
}
