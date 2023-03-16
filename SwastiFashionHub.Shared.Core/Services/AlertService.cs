using SwastiFashionHub.Shared.Core.Models;
using SwastiFashionHub.Shared.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core.Services
{
    public class AlertService : IAlertService
    {
        private const string _defaultId = "default-alert";
        public event Action<Alert> OnAlert;

        /// <summary>
        /// To show an alert message with success color e.g. green
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        public async Task Success(string message, bool keepAfterRouteChange = false, bool autoClose = true)
        {
            await this.Alert(new Alert
            {
                Type = AlertType.Success,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose
            });
        }

        /// <summary>
        /// To show an alert message with danger color, mostly used for error message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        public async Task Danger(string message, bool keepAfterRouteChange = false, bool autoClose = true)
        {
            await this.Alert(new Alert
            {
                Type = AlertType.Error,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose
            });
        }

        /// <summary>
        /// To show an alert message with danger color with a navigation link in message box, mostly used for error message
        /// </summary>
        /// <param name="navigationUrl"></param>
        /// <param name="navigationMessage"></param>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        public async Task DangerWithLink(string navigationUrl, string navigationMessage, string message, bool keepAfterRouteChange = false, bool autoClose = true)
        {
            await this.Alert(new Alert
            {
                Type = AlertType.Error,
                Message = message,
                NavigationURL = navigationUrl,
                NavigationMessage = navigationMessage,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose
            });
        }

        /// <summary>
        /// To show an alert message with information color
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        public async Task Info(string message, bool keepAfterRouteChange = false, bool autoClose = true)
        {
            await this.Alert(new Alert
            {
                Type = AlertType.Info,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose
            });
        }

        /// <summary>
        /// To show an alert message with warning color
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        public async Task Warning(string message, bool keepAfterRouteChange = false, bool autoClose = true)
        {
            await this.Alert(new Alert
            {
                Type = AlertType.Warning,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose
            });
        }

        /// <summary>
        /// To start showing an alert on screen
        /// </summary>
        /// <param name="alert"></param>
        /// <returns></returns>
        public async Task Alert(Alert alert)
        {
            alert.Id = alert.Id ?? _defaultId;
            this.OnAlert?.Invoke(alert);
            await Task.CompletedTask;
        }

        /// <summary>
        /// To clear alert from the screen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Clear(string id = _defaultId)
        {
            this.OnAlert?.Invoke(new Alert { Id = id });
            await Task.CompletedTask;
        }

        /// <summary>
        /// To show an alert message with primary color
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        public async Task Primary(string message, bool keepAfterRouteChange = false, bool autoClose = true)
        {
            await this.Alert(new Alert
            {
                Type = AlertType.Primary,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose
            });
        }

        /// <summary>
        /// To show an alert message with secondary color
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        public async Task Secondary(string message, bool keepAfterRouteChange = false, bool autoClose = true)
        {
            await this.Alert(new Alert
            {
                Type = AlertType.Secondary,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose
            });
        }

        /// <summary>
        /// To show an alert message with light theme color
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        public async Task Light(string message, bool keepAfterRouteChange = false, bool autoClose = true)
        {
            await this.Alert(new Alert
            {
                Type = AlertType.Light,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose
            });
        }

        /// <summary>
        /// To show an alert message with dark theme color
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        public async Task Dark(string message, bool keepAfterRouteChange = false, bool autoClose = true)
        {
            await this.Alert(new Alert
            {
                Type = AlertType.Dark,
                Message = message,
                KeepAfterRouteChange = keepAfterRouteChange,
                AutoClose = autoClose
            });
        }
    }
}
