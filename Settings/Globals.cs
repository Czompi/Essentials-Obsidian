using Essentials.Configs;
using Essentials.Settings.Lang;
using Obsidian.API;
using Obsidian.API.Plugins;
using Obsidian.API.Plugins.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Essentials.Settings
{
    public class Globals
    {
        #region Root
        public static string VersionFull
        {
            get
            {
#if DEBUG
                return $"{Version}-DEBUG";
#elif SNAPSHOT
                return $"{Version}-SNAPSHOT";
#elif RELEASE
                return $"{Version}-RELEASE";
#endif
            }
        }

        public static Version Version
        {
            get
            {
                return PluginInfo.Version;
            }
        }

        public static string RenderColoredChatMessage(string message)
        {
            return string.IsNullOrEmpty(message) ? "" : message.Replace("&", "§");
        }

        public static ILogger Logger { get; internal set; }
        public static IFileReader FileReader { get; internal set; }
        public static IFileWriter FileWriter { get; internal set; }
        public static ConfigManager Configs { get; internal set; }
        public static IPluginInfo PluginInfo { get; internal set; }
        public static LanguageManager Language { get; internal set; }
        #endregion

        #region Files
        public class Files
        {
            public static string WorkingDirectory
            {
                get
                {
                    var dir = Path.Combine("", "Essentials");
                    if (!FileWriter.DirectoryExists(dir)) FileWriter.CreateDirectory(dir);
                    return dir;
                }
            }

            public static string Motd
            {
                get
                {
                    return Path.Combine(WorkingDirectory, "motd.txt");
                }
            }

            public static string Config
            {
                get
                {
                    return Path.Combine(WorkingDirectory, "config.json");
                }
            }

            public static string HomesDir
            {
                get
                {
                    var dir = Path.Combine(WorkingDirectory, "homes");
                    if (!FileWriter.DirectoryExists(dir)) FileWriter.CreateDirectory(dir);
                    return dir;
                }
            }

            public static string LanguageFile(String code)
            {
                return Path.Combine(HomesDir, $"messages_{code}.json");
            }
            
            public static string PlayerHome(Guid uuid)
            {
                return Path.Combine(HomesDir, $"{uuid.ToString().Replace("-", "")}.json");
            }

            public static string WarpsDir
            {
                get
                {
                    var dir = Path.Combine(WorkingDirectory, "warps");
                    if (!FileWriter.DirectoryExists(dir)) FileWriter.CreateDirectory(dir);
                    return dir;
                }
            }

            public static string Warp(string name)
            {
                return Path.Combine(WarpsDir, $"{name.ToLower()}.json");
            }
        }
        #endregion

        #region Defaults
        public class Defaults
        {
            public static string Motd
            {
                get
                {
                    return "&6Welcome, {PLAYER}&6!\n&6Type &c/help&6 for a list of commands.\n&6Type &c/list&6 to see who else is online.\n&6Players online:&c {ONLINE} &6- World time:&c {WORLDTIME12}";
                }
            }

            public static object Config { get; internal set; }
        }
        #endregion

        #region Render command usage
        internal static IChatMessage RenderCommandUsage(string commandUsage, string hoverText = "Click to suggest the command.", ChatColor? color = null)
        {
            color ??= ChatColor.Red;

            var chatMessage = IChatMessage.Simple("");

            #region prefix
            var prefix = IChatMessage.CreateNew();
            prefix.Text = $"{color}Usage: ";
            chatMessage.AddExtra(prefix);
            #endregion

            #region usage
            var usage = IChatMessage.CreateNew();
            var commandSuggest = commandUsage.Contains(" ") ? $"{commandUsage.Split(" ").FirstOrDefault()} " : commandUsage;
            usage.Text = $"{color}{commandUsage}";

            var clickEvent = ITextComponent.CreateNew();
            clickEvent.Action = ETextAction.SuggestCommand;
            clickEvent.Value = $"{commandSuggest}";
            usage.ClickEvent = clickEvent;

            var hoverEvent = ITextComponent.CreateNew();
            hoverEvent.Action = ETextAction.ShowText;
            hoverEvent.Value = $"{hoverText}";
            usage.HoverEvent = hoverEvent;

            chatMessage.AddExtra(usage);
            #endregion

            return chatMessage;
        }
        #endregion

        #region RenderClickableCommand
        internal static IChatMessage RenderClickableCommand(string name, string hoverText = "Click to suggest the command.", string suggestionPrefix = "", ChatColor? color = null)
        {
            color ??= ChatColor.Red;
            var command = name.Contains(" ") ? string.Join(" ", name.Split(" ").Where(x => !x.StartsWith("<"))) + " " : name;
            if (suggestionPrefix != "") command = $"{suggestionPrefix.Trim(' ')} {name}";
            var chatMessage = IChatMessage.CreateNew();
            chatMessage.Text = $"{color}{(suggestionPrefix != "" ? "" : (!name.StartsWith("/") ? "/" : ""))}{name}";

            var clickEvent = ITextComponent.CreateNew();
            clickEvent.Action = ETextAction.SuggestCommand;
            clickEvent.Value = $"{(!command.StartsWith("/") ? "/" : "")}{command}";
            chatMessage.ClickEvent = clickEvent;

            var hoverEvent = ITextComponent.CreateNew();
            hoverEvent.Action = ETextAction.ShowText;
            hoverEvent.Value = $"{hoverText}";
            chatMessage.HoverEvent = hoverEvent;

            return chatMessage;
        }
        #endregion

        #region Command dictionary
        internal static Dictionary<string, string> Commands = new Dictionary<string, string>
        {
            {"/gm <0|1|2|3>", "Switch gamemode."},
            {"/homes", "List all homes."},
            {"/home <name>", "Teleport to your specific home."},
            {"/delhome <name>", "Delete a specific home."},
            {"/sethome", "Create new home."},
            {"/kits", "List kits."},
            {"/kit <name>", "Get specific kit."},
            {"/warps", "List warps."},
            {"/warp <name>", "Teleport to your specific warp."},
            {"/delwarp <name>", "Teleport to your specific warp."},
            {"/setwarp <name>", "Teleport to your specific warp."},
        };
        #endregion

        #region Newtonsoft.Json -> System.Text.Json
        public static JsonSerializerOptions JsonSerializerOptions { 
            get {
                var jso = new JsonSerializerOptions();
                jso.WriteIndented = true;
                jso.PropertyNameCaseInsensitive = false;
                jso.IgnoreNullValues = true;
                //jso.ReadCommentHandling = JsonCommentHandling.Allow;
                jso.ReadCommentHandling = JsonCommentHandling.Skip;
                return jso;
            } 
        }

        #endregion
    }
}