using System;

namespace Essentials.Commands
{
    internal class MemoryFormatter
    {
        internal static String Fancy(long memory)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            int order = 0;
            while (memory >= 1024 && order < sizes.Length - 1)
            {
                order++;
                memory = memory / 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            return String.Format("{0:0.##} {1}", memory, sizes[order]);
        }
    }
}