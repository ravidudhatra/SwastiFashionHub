using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using System.Text.Json;

namespace SwastiFashionHub.Shared.Core.Extensions
{
    public class LocalStorage
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorage(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task SetAsync<T>(string key, T value)
        {
            string jsVal = null;
            if (value != null)
                jsVal = JsonSerializer.Serialize(value);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem",
                new object[] { key, jsVal });
        }

        public async Task<(T, bool)> GetAsync<T>(string key)
        {
            try
            {
                string val = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
                if (val == null) return default;
                T result = JsonSerializer.Deserialize<T>(val);
                return (result, false);
            }
            catch
            {
                return (default, true);
            }
        }
        public async Task RemoveAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
        public async Task ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.clear");
        }

    }
}
