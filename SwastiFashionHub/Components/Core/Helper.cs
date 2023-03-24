using System.Globalization;
using System.Text.RegularExpressions;

namespace SwastiFashionHub.Components.Core
{
    public static class Helper
    {
        /// <summary>
        /// Return property value from object with property name
        /// </summary>
        /// <typeparam name="T">The type of the property</typeparam>
        /// <param name="item">The object to read proprety from</param>
        /// <param name="propName">The property name</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(object item, string propName)
        {
            return (T)item.GetType().GetProperty(propName).GetValue(item, null);
        }



        /// <summary>
        /// Set given value to object
        /// </summary>
        /// <typeparam name="T">the type of value</typeparam>
        /// <param name="item">The object</param>
        /// <param name="propName">Property name to set</param>
        /// <param name="value">the value to set</param>
        public static void SetPropertyValue<T>(object item, string propName, object value)
        {
            item.GetType().GetProperty(propName).SetValue(item, (T)value);
        }



        /// <summary>
        /// Validates password strength
        /// </summary>
        /// <param name="value">The password value</param>
        /// <returns></returns>
        public static string CheckPasswordStrength(string value)
        {
            string errorMsg = "";

            if (String.IsNullOrEmpty(value)) return "Mandatory field";

            if (value.Trim().Length <= 12) return "Password length must be more than 12 characters";

            // One upper case
            if (!value.Any(char.IsUpper)) return "Password must have atleast one uppercase character";

            // One lower case
            if (!value.Any(char.IsLower)) return "Password must have atleast one lowercase character";

            // No whitespace
            if (value.Contains(" ")) return "Password can not have whitespace character";

            // Should have alteast one digit
            if (!value.Any(c => char.IsDigit(c))) return "Password must have atleast one digit(0-9)";

            // Should have a special character
            string specialChar = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialCharArray = specialChar.ToCharArray();
            bool hasSpecialChar = false;
            foreach (char c in specialCharArray)
            {
                if (value.Contains(c))
                {
                    hasSpecialChar = true;
                    break;
                }
            }
            if (!hasSpecialChar)
            {
                return "Password must have atleast one special character";
            }

            // Return			
            return "";
        }



        /// <summary>
        /// Validaes email address
        /// </summary>
        /// <param name="email">the email to be tested</param>
        /// <returns></returns>
        public static bool ValidateEmailAddress(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Validates phone number
        /// </summary>
        /// <param name="phone">The phone number to be validated</param>
        /// <returns></returns>
        public static bool ValidatePhoneNumber(string phone)
        {
            // Check phone number length
            if (phone.Length < 10) return false;

            try
            {
                string txt = Convert.ToInt64(phone).ToString("###-###-####");

            }
            catch (Exception ex)
            {
                return false;
            }

            if (Regex.IsMatch(phone, "000 000 0000")) return false;

            Regex regex = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
            return regex.IsMatch(phone);
        }
    }
}
