using SwastiFashionHub.Shared.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core.Services.Interface
{
    public interface IConfirmService
    {
        event Action<ConfirmServiceModel> OnConfirmChange;

        /// <summary>
        /// To clear confirm dialogue from screen
        /// </summary>
        /// <returns></returns>
        Task Clear();

        /// <summary>
        /// To set dialogue options and invoke on show
        /// </summary>
        /// <param name="confirmServiceModel"></param>
        /// <returns></returns>
        Task Confirm(ConfirmServiceModel confirmServiceModel);

        /// <summary>
        /// Show a confirm dialogue with customized message and button click events
        /// </summary>
        /// <param name="message"></param>
        /// <param name="saveButtonName"></param>
        /// <param name="saveButton"></param>
        /// <param name="discardButtonName"></param>
        /// <param name="discardButton"></param>
        /// <returns></returns>
        Task Show(string message, string saveButtonName, Action saveButton, string discardButtonName = null, Action discardButton = null);
    }
}
