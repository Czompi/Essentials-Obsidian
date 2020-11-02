using Essentials.Configs;
using Essentials.Plugin;
using Obsidian.API.Plugins.Services;
using System;
using System.Collections.Generic;
using System.IO;

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
/*#elif SNAPSHOT
                return $"{Version}-SNAPSHOT";*/
#elif RELEASE
                return $"{Version}-RELEASE";
#endif
            }
        }

        public static Version Version
        {
            get
            {
                return new Version("0.0.1");
            }
        }

        public static string RenderColoredChatMessage(string message)
        {
            return message.Replace("&", "§");
        }

        public static ILogger Logger { get; internal set; }
        public static IFileReader FileReader { get; internal set; }
        public static IFileWriter FileWriter { get; internal set; }
        public static ConfigManager Configs { get; internal set; }
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

            public static string PlayerHomes(Guid uuid)
            {
                return Path.Combine(HomesDir, $"{uuid.ToString().Replace("-", "")}.json");
            }
            
            public static string WarpsDir
            {
                get
                {
                    var dir = Path.Combine(WorkingDirectory, "warps");
                    if (FileWriter.DirectoryExists(dir)) FileWriter.CreateDirectory(dir);
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
        }
        #endregion

        #region Command usage render by command name
        private ChatMessage SendCommandUsage(string command)
        {
            var currentCommand = Commands.Where(x => x.Key.ToLower().StartsWith(command.ToLower())).FirstOrDefault();
            var commands = ChatMessage.Simple("");
            var commandSuggest = currentCommand.Key.Contains(" ") ? string.Join(" ", currentCommand.Key.Split(" ").Where(x => !x.StartsWith("<"))) + " " : currentCommand.Key;
            var usage = new ChatMessage
            {
                Text = $"{ChatColor.Red}/{currentCommand.Key}",
                ClickEvent = new TextComponent
                {
                    Action = ETextAction.SuggestCommand,
                    Value = $"/{commandSuggest}"
                },
                HoverEvent = new TextComponent
                {
                    Action = ETextAction.ShowText,
                    Value = $"Click to suggest the command"
                }
            };

            var prefix = new ChatMessage
            {
                Text = $"{ChatColor.Red}Usage: "
            };

            commands.AddExtra(prefix);
            commands.AddExtra(usage);
            return commands;
        }
        #endregion

        #region Command dictionary
        private Dictionary<string, string> Commands = new Dictionary<string, string>
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
    }
}