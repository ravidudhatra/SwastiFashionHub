using SwastiFashionHub.Shared.Core.Models;
using SwastiFashionHub.Shared.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core.Services
{
    public class ConfirmService : IConfirmService
    {
        public event Action<ConfirmServiceModel> OnConfirmChange;

        /// <summary>
        /// Show a confirm dialogue with customized message and button click events
        /// </summary>
        /// <param name="message"></param>
        /// <param name="saveButtonName"></param>
        /// <param name="saveButton"></param>
        /// <param name="discardButtonName"></param>
        /// <param name="discardButton"></param>
        /// <returns></returns>
        public async Task Clear()
        {
            this.OnConfirmChange?.Invoke(null);
            await Task.CompletedTask;
        }

        /// <summary>
        /// To set dialogue options and invoke on show
        /// </summary>
        /// <param name="confirmServiceModel"></param>
        /// <returns></returns>
        public async Task Confirm(ConfirmServiceModel confirmService)
        {
            this.OnConfirmChange?.Invoke(confirmService);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Show a confirm dialogue with customized message and button click events
        /// </summary>
        /// <param name="message"></param>
        /// <param name="saveButtonName"></param>
        /// <param name="saveButton"></param>
        /// <param name="discardButtonName"></param>
        /// <param name="discardButton"></param>
        /// <returns></returns>
        public async Task Show(string message, string saveButtonName, Action saveButton, string discardButtonName = null, Action discardButton = null)
        {
            await this.Confirm(new ConfirmServiceModel
            {
                DiscardButton = discardButton,
                DiscardButtonName = discardButtonName,
                Message = message,
                SaveButton = saveButton,
                SaveButtonName = saveButtonName
            });
        }
    }
}
