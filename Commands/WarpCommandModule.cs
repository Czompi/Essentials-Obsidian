using Essentials.Configs;
using Essentials.Extensions;
using Essentials.Settings;
using Obsidian.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Essentials.Commands
{
    public class WarpCommandModule
    {
        #region /warp <name>
        [Command("warp")]
        [CommandInfo("Switch gamemode.", "/warp <name>")]
        public async Task WarpAsync(CommandContext Context) => await Context.Player.SendMessageAsync(Globals.RenderCommandUsage("/warp <name>"));

        [CommandOverload]
        public async Task WarpAsync(CommandContext Context, [Remaining] string args_)
        {
            var chatMessage = IChatMessage.Simple("");
            var args = args_.Contains(" ") ? args_.Split(" ").ToList() : new List<string> { args_ };
            if (args.Count == 1)
            {
                var warp = Globals.Configs.Warps[args[0]];
                try
                {
                    await Context.Player.TeleportAsync(warp.Position.ToObsidianPosition());

                    chatMessage.AddExtra(IChatMessage.Simple($"Successfully warped to {ChatColor.BrightGreen}{warp.Name}{ChatColor.Reset}."));
                }
                catch (Exception ex)
                {
                    chatMessage.AddExtra(IChatMessage.Simple($"Cannot warp to {ChatColor.Red}{warp.Name}{ChatColor.Reset}!"));
                    if (Context.Player.IsOperator) chatMessage.AddExtra(IChatMessage.Simple($" For more information, see console."));
                    Globals.Logger.LogError($"{ChatColor.Red}{Context.Player.Username}{ChatColor.Reset} cannot warp to {ChatColor.Red}{warp.Name}{ChatColor.Reset}.\n{ex.ToString()}");
                }
            }
            else
            {
                chatMessage = Globals.RenderCommandUsage("/warp <name>");
            }
            await Context.Player.SendMessageAsync(chatMessage);
        }
        #endregion

        #region /warps
        [Command("warps")]
        [CommandInfo("List all warps.")]
        public async Task WarpsAsync(CommandContext Context)
        {
            var chatMessage = IChatMessage.Simple($"{ChatColor.Reset}Warps: ");
            var warps = IChatMessage.Simple("");
            var warpList = Globals.Configs.Warps;
            int i = 0;
            foreach (var warpData in warpList)
            {
                var warp = warpData.Value;
                warps.AddExtra(Globals.RenderClickableCommand(warp.Name, "Click to navigate to warp", suggestionPrefix: "/warp", ChatColor.BrightGreen));
                if (i + 1 < warpList.Count) warps.AddExtra(IChatMessage.Simple($"{ChatColor.Reset}, "));
            }
            chatMessage.AddExtra(warps);
            await Context.Player.SendMessageAsync(chatMessage);
        }

        [CommandOverload]
        public async Task WarpsAsync(CommandContext Context, [Remaining] string args_) => await Context.Player.SendMessageAsync(Globals.RenderCommandUsage("/warps"));
        #endregion

        #region /setwarp <name>
        [Command("setwarp")]
        [CommandInfo("Switch gamemode.", "/setwarp <name>")]
        public async Task SetWarpAsync(CommandContext Context) => await Context.Player.SendMessageAsync(Globals.RenderCommandUsage("/setwarp <name>"));

        [CommandOverload]
        public async Task SetWarpAsync(CommandContext Context, [Remaining] string args_)
        {
            var chatMessage = IChatMessage.Simple("");
            var args = args_.Contains(" ") ? args_.Split(" ").ToList() : new List<string> { args_ };
            if (args.Count == 1)
            {
                var warp = args[0];
                var key = warp.ReplaceSpecialCharactersWith("_");
                try
                {
                    var cfg = new WarpConfig
                    {
                        LastOwner = Context.Player.Uuid,
                        Name = warp,
                        World = Context.Player.WorldLocation.Name,
                        Position = Context.Player.Position.ToEssentialsPosition()
                    };

                    if (Globals.Configs.AddWarp(warp, cfg, Context.Player))
                        chatMessage.AddExtra(IChatMessage.Simple($"Successfully created {ChatColor.BrightGreen}{cfg.Name}{ChatColor.Reset} warp on location {cfg.Position.ToColoredString(ChatColor.BrightGreen)}{ChatColor.Reset}."));
                    else
                        chatMessage.AddExtra(IChatMessage.Simple($"Cannot created {ChatColor.Red}{warp}{ChatColor.Reset} warp on location {cfg.Position.ToColoredString(ChatColor.Red)}{ChatColor.Reset}."));
                }
                catch (Exception ex)
                {
                    chatMessage.AddExtra(IChatMessage.Simple($"Cannot create {ChatColor.Red}{warp}{ChatColor.Reset} warp!"));
                    if (Context.Player.IsOperator) chatMessage.AddExtra(IChatMessage.Simple($" For more information, see console."));
                    Globals.Logger.LogError($"{ChatColor.Red}{Context.Player.Username}{ChatColor.Reset} cannot warp to {ChatColor.Red}{warp}{ChatColor.Reset}.\n{ex.ToString()}");
                }
            }
            else
            {
                chatMessage = Globals.RenderCommandUsage("/setwarp <name>");
            }
            await Context.Player.SendMessageAsync(chatMessage);
        }
        #endregion

    }
}
