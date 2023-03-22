using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwastiFashionHub.Shared.Core.Extensions
{
    public static class FileUploadExtension
    {
        public static List<string> ImageExt = new() { ".jpg", ".jpeg", ".png", ".gif" };

        /// <summary>
        /// To validate and identify the given url contains an image or not
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Logger"></param>
        /// <returns></returns>
        public static async Task<bool> IsUrlImage(this string url, ILogger Logger)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                var contentType = response?.Content?.Headers?.ContentType?.MediaType.ToLower(CultureInfo.InvariantCulture);
                if (contentType != null && (contentType.StartsWith("image/") || contentType.Contains("octet-stream")))
                    return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }
            return false;
        }

        /// <summary>
        /// Get extension from the given file URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetFileExtensionFromUrl(this string url)
        {
            url = url.Split('?')[0];
            url = url.Split('/').Last();
            return url.Contains('.') ? url.Substring(url.LastIndexOf('.')) : "";
        }

        /// <summary>
        /// Trim and add ellipsis at the end for long string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FixedLongString(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && value.Length > 20)
            {
                return value.Substring(0, 5) + "....." + value.Substring(value.Length - 5, 5);
            }
            return value;
        }

        /// <summary>
        /// To get an unique file name with same extension of given file name
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetUniqueFileName(this string fileName)
        {
            string extension = Path.GetExtension(fileName);
            return Guid.NewGuid().ToString() + extension ?? string.Empty;
        }

        /// <summary>
        /// To identify the media type (MIME Type) is an image or not
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public static bool IsMediaTypeImage(this string mediaType)
        {
            if (string.IsNullOrWhiteSpace(mediaType))
                return false;
            else if (mediaType.StartsWith("image/"))
                return true;
            else if (mediaType.Contains("octet-stream"))
                return true;
            return false;
        }

        /// <summary>
        /// Get a format a date in MMM dd, yyyy hh:mm:tt format
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string GetFormattedDateTime(this DateTime datetime)
            => datetime.ToString("MMM dd, yyyy hh:mm tt");
    }
}
