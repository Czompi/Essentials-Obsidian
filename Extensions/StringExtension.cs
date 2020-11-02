using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Obsidian.Commands
{
    public static class StringExtension
    {
        public static bool IsInteger(this string value)
        {
            return !string.IsNullOrEmpty(value) && value.All(Char.IsDigit);
        }
    }
}
