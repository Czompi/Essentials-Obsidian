using Essentials.Settings;
using Obsidian.API;
using Obsidian.CommandFramework;
using Obsidian.CommandFramework.Attributes;
using Obsidian.CommandFramework.Entities;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Essentials.Commands
{
    public class StatisticsCommandModule : BaseCommandClass
    {

        #region /gc
        [Command("gc", "tps")]
        [CommandInfo("Stats.", "/gc")]
        public async Task GcAsync(ObsidianContext Context)
        {
            var chatMessage = IChatMessage.Simple("");

            Process currentProcess = Process.GetCurrentProcess();

            //long maxMemory = currentProcess.VirtualMemorySize64;
            //long totalMemory = currentProcess.PeakVirtualMemorySize64;
            //long freeMemory = currentProcess.PeakVirtualMemorySize64-currentProcess.VirtualMemorySize64;
            long maxMemory = currentProcess.WorkingSet64;
            long totalMemory = currentProcess.PeakWorkingSet64;
            long freeMemory = totalMemory - maxMemory;
            //long usedMemory = currentProcess.PrivateMemorySize64;
            //long maxMemory = currentProcess.WorkingSet64;

            chatMessage.AddExtra(IChatMessage.Simple($"{Globals.Language.TranslateMessage("gcfree", MemoryFormatter.Fancy(freeMemory, MemoryFormatter.EMemory.MB, false))}\n"));
            chatMessage.AddExtra(IChatMessage.Simple($"{Globals.Language.TranslateMessage("gcmax", MemoryFormatter.Fancy(maxMemory, MemoryFormatter.EMemory.MB, false))}\n"));
            chatMessage.AddExtra(IChatMessage.Simple($"{Globals.Language.TranslateMessage("gctotal", MemoryFormatter.Fancy(totalMemory, MemoryFormatter.EMemory.MB, false))}\n"));
            chatMessage.AddExtra(IChatMessage.Simple($"{Globals.Language.TranslateMessage("tps", Context.Server.TPS)}"));
#if DEBUG
            chatMessage.AddExtra(IChatMessage.Simple($"{ChatColor.DarkGreen}//There are a lot of things here. :o"));
#endif

            await Context.Player.SendMessageAsync(chatMessage);
        }
        

        [CommandOverload]
        public async Task GcAsync(ObsidianContext Context, [Remaining] string args_) => await Context.Player.SendMessageAsync(Globals.RenderCommandUsage("/gc"));
        #endregion

    }
}
