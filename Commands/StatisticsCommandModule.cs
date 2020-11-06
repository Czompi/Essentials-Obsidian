using Essentials.Settings;
using Obsidian.API;
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

            long usedMemory = currentProcess.PrivateMemorySize64;
            long maxMemory = currentProcess.WorkingSet64;

            chatMessage.AddExtra(IChatMessage.Simple($"Used RAM: §3{MemoryFormatter.Fancy(usedMemory)}\n"));
            chatMessage.AddExtra(IChatMessage.Simple($"Max RAM: §3{MemoryFormatter.Fancy(maxMemory)}"));

            await Context.Player.SendMessageAsync(chatMessage);
        }
        

        [CommandOverload]
        public async Task GcAsync(ObsidianContext Context, [Remaining] string args_) => await Context.Player.SendMessageAsync(Globals.RenderCommandUsage("/gc"));
        #endregion

    }
}
