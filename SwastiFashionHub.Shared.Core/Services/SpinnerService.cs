using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core.Services
{
    public class SpinnerService
    {
        public event Action OnShow;
        public event Action OnHide;

        /// <summary>
        /// To show spinner on the center of the screen for loading a screen or data
        /// </summary>
        /// <returns></returns>
        public async Task Show()
        {
            OnShow?.Invoke();
            await Task.CompletedTask;
        }

        /// <summary>
        /// To Hide spinner from the screen and end of the loading a screen
        /// </summary>
        /// <returns></returns>
        public async Task Hide()
        {
            OnHide?.Invoke();
            await Task.CompletedTask;
        }
    }
}
