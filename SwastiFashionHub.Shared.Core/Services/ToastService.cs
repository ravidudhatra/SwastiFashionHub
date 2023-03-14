using SwastiFashionHub.Shared.Core.Common;
using System.Timers;

namespace SwastiFashionHub.Shared.Core.Services
{
    public class ToastService : IDisposable
    {
        public event Action<string, ToastLevel> OnShow;
        public event Action OnHide;
        private System.Timers.Timer Countdown;
        
        public async Task ShowToast(string message, ToastLevel level)
        {
            OnShow?.Invoke(message, level);
            StartCountdown();
            await Task.CompletedTask;
        }

        private void StartCountdown()
        {
            SetCountdown();

            if (Countdown.Enabled)
            {
                Countdown.Stop();
                Countdown.Start();
            }
            else
            {
                Countdown.Start();
            }
        }

        private void SetCountdown()
        {
            if (Countdown == null)
            {
                Countdown = new System.Timers.Timer(5000);
                Countdown.Elapsed += HideToast;
                Countdown.AutoReset = false;
            }
        }

        private void HideToast(object source, ElapsedEventArgs args)
        {
            OnHide?.Invoke();
        }

        public void Dispose()
        {
            Countdown?.Dispose();
        }
    }
}
