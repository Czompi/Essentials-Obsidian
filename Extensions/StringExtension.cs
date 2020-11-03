using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Obsidian.Commands
{
    public static class StringExtension
    {
        public static bool IsInteger(this string value) => !string.IsNullOrEmpty(value) && value.All(Char.IsDigit);
        public static string TrimSpecialCharacters(this string value) => ReplaceSpecialCharactersWith(value, string.Empty);
        public static string ReplaceSpecialCharactersWith(this string value, string replaceWith = "_") => new Regex("([^a-zA-Z])").Replace(value, replaceWith);
    }
}
