using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SwastiFashionHub.Shared.Core.Services.Interface;

using Model = SwastiFashionHub.Shared.Core.Models;

namespace SwastiFashionHub.Shared
{
    public partial class Alert : IDisposable
    {
        [Inject]
        private IAlertService AlertService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; } = "default-alert";
        [Parameter]
        public bool Fade { get; set; } = true;

        private List<Model.Alert> Alerts = new List<Model.Alert>();

        [Inject]
        private ILocalStorageService LocalStorage { get; set; }

        protected override void OnInitialized()
        {
            // subscribe to new alerts and location change events
            AlertService.OnAlert += OnAlert;
            NavigationManager.LocationChanged += OnLocationChange;
        }

        /// <summary>
        /// Dispose unused objects to release memory
        /// </summary>
        public void Dispose()
        {
            // unsubscribe from alerts and location change events
            AlertService.OnAlert -= OnAlert;
            NavigationManager.LocationChanged -= OnLocationChange;
        }

        /// <summary>
        /// To show an alert message
        /// </summary>
        /// <param name="alert"></param>
        private async void OnAlert(Model.Alert alert)
        {
            // ignore alerts sent to other alert components
            if (alert.Id != Id)
                return;

            // clear alerts when an empty alert is received
            if (alert.Message == null)
            {
                // remove alerts without the 'KeepAfterRouteChange' flag set to true
                if (Alerts != null && Alerts.Count > 0)
                    Alerts.RemoveAll(x => !x.KeepAfterRouteChange);

                // set the 'KeepAfterRouteChange' flag to false for the 
                // remaining alerts so they are removed on the next clear
                if (Alerts != null && Alerts.Count > 0)
                    Alerts.ForEach(x => x.KeepAfterRouteChange = false);
            }
            else
            {
                // add alert to array
                if (alert != null)
                    Alerts.Add(alert);
                //StateHasChanged();
                await InvokeAsync(() => { StateHasChanged(); });

                // auto close alert if required
                if (alert.AutoClose)
                {
                    await Task.Delay(5000);
                    RemoveAlert(alert);
                }
            }

            //StateHasChanged();
            await InvokeAsync(() => { StateHasChanged(); });
        }

        /// <summary>
        /// To clear alert message when location changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnLocationChange(object sender, LocationChangedEventArgs e)
        {
            _ = AlertService.Clear(Id);
        }

        /// <summary>
        /// Remove alert from screen
        /// </summary>
        /// <param name="alert"></param>
        private async void RemoveAlert(Model.Alert alert)
        {
            try
            {
                // check if already removed to prevent error on auto close
                if (Alerts != null && Alerts.Count > 0 && !Alerts.Contains(alert)) return;

                if (Fade)
                {
                    // fade out alert
                    alert.Fade = true;

                    // remove alert after faded out
                    await Task.Delay(250);
                    if (Alerts != null && Alerts.Count > 0)
                        Alerts.Remove(alert);
                }
                else
                {
                    // remove alert
                    if (Alerts != null && Alerts.Count > 0)
                        Alerts.Remove(alert);
                }
            }
            catch (Exception)
            {
            }

            //StateHasChanged();
            await InvokeAsync(() => { StateHasChanged(); });
        }

        /// <summary>
        /// Apply style to alert based on the alert message type
        /// </summary>
        /// <param name="alert"></param>
        /// <returns></returns>
        private string CssClass(Model.Alert alert)
        {
            if (alert == null) return null;

            var classes = new List<string> { "frame-toast", "show" };

            var alertTypeClass = new Dictionary<Model.AlertType, string>();
            alertTypeClass[Model.AlertType.Success] = "btn-success";
            alertTypeClass[Model.AlertType.Error] = "btn-danger";
            alertTypeClass[Model.AlertType.Info] = "btn-info";
            alertTypeClass[Model.AlertType.Primary] = "btn-primary";
            alertTypeClass[Model.AlertType.Secondary] = "btn-secondary";
            alertTypeClass[Model.AlertType.Warning] = "btn-warning";
            alertTypeClass[Model.AlertType.Light] = "btn-light";
            alertTypeClass[Model.AlertType.Dark] = "btn-dark";

            classes.Add(alertTypeClass[alert.Type]);

            if (alert.Fade)
                classes.Add("fade");

            return string.Join(' ', classes);
        }

        /// <summary>
        /// Navigate to specific page, this option is available with alert message
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task NavigationOnClick(string url)
        {
            if (!string.IsNullOrWhiteSpace(url) && url.Contains("login"))
                await LocalStorage.RemoveItemAsync("TOKEN_JWT");
            NavigationManager.NavigateTo(url, true);
        }
    }

}
