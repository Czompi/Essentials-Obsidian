using System;

namespace Essentials.Commands
{
    internal class MemoryFormatter
    {
        internal static String Fancy(long memory, EMemory maxDisplayableAmount = EMemory.EB, bool showAmountShort = true)
        {
            string[] sizes = Enum.GetNames(typeof(EMemory));
            int order = 0;
            while (memory >= 1024 && order < sizes.Length - 1 && ((int)maxDisplayableAmount) > order)
            {
                order++;
                memory /= 1024;
            }

            // Adjust the format string to your preferences. For example "{0:0.#}{1}" would
            // show a single decimal place, and no space.
            if(showAmountShort) return String.Format("{0:0.##} {1}", memory, sizes[order]);
            else return String.Format("{0:0.##}", memory);
        }

        internal enum EMemory
        {
            B,
            KB,
            MB,
            GB,
            TB,
            PB,
            EB
        }
    }
}