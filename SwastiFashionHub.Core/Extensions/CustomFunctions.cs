using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SwastiFashionHub.Core.Extensions
{
    public static class CustomFunctions
    {
        public static bool Like(string input, string pattern, char escapeChar = '\\')
        {
            var escapeString = escapeChar.ToString();
            var patternString = pattern
                .Replace(escapeString, escapeString + escapeString)
                .Replace("%", ".*")
                .Replace("_", ".");
            var regex = new Regex("^" + patternString + "$");
            return regex.IsMatch(input);
        }
    }
}
