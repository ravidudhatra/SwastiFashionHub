using SwastiFashionHub.Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core.Services.Interface
{
    public interface IAlertService
    {
        event Action<Alert> OnAlert;
        /// <summary>
        /// To show an alert message with primary color
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        Task Primary(string message, bool keepAfterRouteChange = false, bool autoClose = true);

        /// <summary>
        /// To show an alert message with secondary color
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        Task Secondary(string message, bool keepAfterRouteChange = false, bool autoClose = true);

        /// <summary>
        /// To show an alert message with light theme color
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        Task Light(string message, bool keepAfterRouteChange = false, bool autoClose = true);

        /// <summary>
        /// To show an alert message with dark theme color
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        Task Dark(string message, bool keepAfterRouteChange = false, bool autoClose = true);

        /// <summary>
        /// To show an alert message with success color e.g. green
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        Task Success(string message, bool keepAfterRouteChange = false, bool autoClose = true);

        /// <summary>
        /// To show an alert message with danger color, mostly used for error message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        Task Danger(string message, bool keepAfterRouteChange = false, bool autoClose = true);

        /// <summary>
        /// To show an alert message with information color
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        Task Info(string message, bool keepAfterRouteChange = false, bool autoClose = true);

        /// <summary>
        /// To show an alert message with warning color
        /// </summary>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        Task Warning(string message, bool keepAfterRouteChange = false, bool autoClose = true);

        /// <summary>
        /// To show an alert message with danger color with a navigation link in message box, mostly used for error message
        /// </summary>
        /// <param name="navigationUrl"></param>
        /// <param name="navigationMessage"></param>
        /// <param name="message"></param>
        /// <param name="keepAfterRouteChange"></param>
        /// <param name="autoClose"></param>
        /// <returns></returns>
        Task DangerWithLink(string navigationUrl, string navigationMessage, string message, bool keepAfterRouteChange = false, bool autoClose = true);

        /// <summary>
        /// To start showing an alert on screen
        /// </summary>
        /// <param name="alert"></param>
        /// <returns></returns>
        Task Alert(Alert alert);

        /// <summary>
        /// To clear alert from the screen
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Clear(string id = null);
    }
}
