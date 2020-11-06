using System.Linq;
using System.Text.RegularExpressions;

namespace Essentials.Extensions
{
    public static class StringExtension
    {
        public static bool IsInteger(this string value) => !string.IsNullOrEmpty(value) && value.All(char.IsDigit);
        public static string TrimSpecialCharacters(this string value) => ReplaceSpecialCharactersWith(value, string.Empty);
        public static string ReplaceSpecialCharactersWith(this string value, string replaceWith = "_") => new Regex("([^a-zA-Z])").Replace(value, replaceWith);
    }
}
