using Essentials.Settings;
using Obsidian.API;
using Obsidian.API.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Essentials.Extensions
{
    public static class EssentialsExtensions
    {

        #region Essentials and Obsidian position conversion
        public static Configs.Position ToEssentialsPosition(this Obsidian.API.Position position) => new Configs.Position { X = position.X, Y = position.Y, Z = position.Z };
        public static Obsidian.API.Position ToObsidianPosition(this Configs.Position position) => new Obsidian.API.Position(position.X, position.Y, position.Z);
        #endregion

        #region Position ToColoredString()
        public static string ToColoredString(this Obsidian.API.Position position) => $"{ChatColor.Reset}X = §9{position.X}{ChatColor.Reset}, Y = §9{position.Y}{ChatColor.Reset}, Z = §9{position.Z}{ChatColor.Reset}";
        public static string ToColoredString(this Obsidian.API.Position position, ChatColor color) => $"{ChatColor.Reset}X = {color}{position.X}{ChatColor.Reset}, Y = {color}{position.Y}{ChatColor.Reset}, Z = {color}{position.Z}{ChatColor.Reset}";

        public static string ToColoredString(this Configs.Position position) => $"{ChatColor.Reset}X = §9{position.X}{ChatColor.Reset}, Y = §9{position.Y}{ChatColor.Reset}, Z = §9{position.Z}{ChatColor.Reset}";
        public static string ToColoredString(this Configs.Position position, ChatColor color) => $"{ChatColor.Reset}X = {color}{position.X}{ChatColor.Reset}, Y = {color}{position.Y}{ChatColor.Reset}, Z = {color}{position.Z}{ChatColor.Reset}";
        #endregion

        #region replace keywords
        public static string ReplaceKeywords(this string str, IPlayer user)
        {
            //string keyword = matchTokens[0];
            var ret = str.Replace("{PLAYER}", user.Username);
            ret = ret.Replace("{ONLINE}", $"{user.Server.Players.Count()}");
            ret = ret.Replace("{WORLDTIME12}", $"{user.WorldLocation.Time.ToString()[0..1]}:{user.WorldLocation.Time.ToString()[2..3]}");
            ret = ret.Replace("{WORLDTIME24}", $"{user.WorldLocation.Time.ToString()[0..1]}:{user.WorldLocation.Time.ToString()[2..3]}");
            return ret;
        }
        public static string ReplaceKeyword(this string keyword, IPlayer user)
        {

            try
            {
                string replacer = null;
                KeywordType validKeyword = (KeywordType)Enum.Parse(typeof(KeywordType), keyword);
                /*if (validKeyword.GetType().Equals(KeywordCachable.CACHEABLE) && keywordCache.ContainsKey(validKeyword))
                {
                    replacer = keywordCache.get(validKeyword).tostring();
                }
                else if (validKeyword.getType().equals(KeywordCachable.SUBVALUE))
                {
                    string subKeyword = "";
                    if (matchTokens.length > 1)
                    {
                        subKeyword = matchTokens[1].toLowerCase(Locale.ENGLISH);
                    }

                    if (keywordCache.containsKey(validKeyword))
                    {
                        Dictionary<string, string> values = (Dictionary<string, string>)keywordCache.get(validKeyword);
                        if (values.containsKey(subKeyword))
                        {
                            replacer = values.get(subKeyword);
                        }
                    }
                }

                if (validKeyword.isPrivate() && !includePrivate)
                {
                    replacer = "";
                }*/

                if (replacer == null)
                {
                    replacer = "";
                    switch (validKeyword)
                    {
                        case KeywordType.PLAYER:
                        case KeywordType.DISPLAYNAME:
                            if (user != null)
                            {
                                replacer = user.Username; // set to displayname
                            }
                            break;
                        case KeywordType.USERNAME:
                            if (user != null)
                            {
                                replacer = user.Username;
                            }
                            break;
                        /*case KeywordType.BALANCE:
                            if (user != null)
                            {
                                replacer = NumberUtil.displayCurrency(user.getMoney(), ess);
                            }
                            break;
                        case KeywordType.MAILS:
                            if (user != null)
                            {
                                replacer = Integer.tostring(user.getMails().size());
                            }
                            break;*/
                        case KeywordType.WORLD:
                        case KeywordType.WORLDNAME:
                            if (user != null)
                            {
                                IWorld location = user.WorldLocation;
                                replacer = location == null ? "" : location.Name;
                            }
                            break;
                        case KeywordType.ONLINE:
                            int playerHidden = 0;
                            /*foreach (IPlayer u in Globals.Server.Players)
                            {
                                if (u.isHidden)
                                {
                                    playerHidden++;
                                }
                            }*/
                            replacer = (Globals.Server.Players.Count() - playerHidden).ToString();
                            break;
                        case KeywordType.UNIQUE:
                            //replacer = NumberFormat.getInstance().format(ess.getUserMap().getUniqueUsers());
                            replacer = (Globals.Server.Players.Count()).ToString();
                            break;
                        case KeywordType.WORLDS:
                            /*StringBuilder worldsBuilder = new StringBuilder();
                            foreach (IWorld w in Globals.Server.Worlds)
                            {
                                if (worldsBuilder.Length > 0)
                                {
                                    worldsBuilder.Append(", ");
                                }
                                worldsBuilder.Append(w.Name);
                            }
                            replacer = worldsBuilder.ToString();*/
                            replacer = Globals.Server.DefaultWorld.Name;
                            break;
                        case KeywordType.PLAYERLIST:
                            /*Dictionary<string, string> outputList;
                            if (keywordCache.containsKey(validKeyword))
                            {
                                outputList = (Dictionary<string, string>)keywordCache.get(validKeyword);
                            }
                            else
                            {
                                bool showHidden;
                                if (user == null)
                                {
                                    showHidden = true;
                                }
                                else
                                {
                                    showHidden = user.isAuthorized("essentials.list.hidden") || user.canInteractVanished();
                                }

                                //First lets build the per group playerlist
                                Dictionary<string, List<User>> playerList = PlayerList.getPlayerLists(ess, user, showHidden);
                                outputList = new HashDictionary<>();
                                for (string groupName : playerList.keySet())
                                {
                                    List<User> groupUsers = playerList.get(groupName);
                                    if (groupUsers != null && !groupUsers.isEmpty())
                                    {
                                        outputList.put(groupName, PlayerList.listUsers(ess, groupUsers, " "));
                                    }
                                }

                                //Now lets build the all user playerlist
                                stringBuilder playerlistBuilder = new stringBuilder();
                                for (Player p : ess.getOnlinePlayers())
                                {
                                    if (ess.getUser(p).isHidden())
                                    {
                                        continue;
                                    }
                                    if (playerlistBuilder.length() > 0)
                                    {
                                        playerlistBuilder.append(", ");
                                    }
                                    playerlistBuilder.append(p.getDisplayName());
                                }
                                outputList.put("", playerlistBuilder.tostring());
                                keywordCache.put(validKeyword, outputList);
                            }

                            //Now thats all done, output the one we want and cache the rest.
                            if (matchTokens.length == 1)
                            {
                                replacer = outputList.get("");
                            }
                            else if (outputList.containsKey(matchTokens[1].toLowerCase(Locale.ENGLISH)))
                            {
                                replacer = outputList.get(matchTokens[1].toLowerCase(Locale.ENGLISH));
                            }
                            else if (matchTokens.length > 2)
                            {
                                replacer = matchTokens[2];
                            }

                            keywordCache.put(validKeyword, outputList);*/
                            replacer = string.Join(", ", Globals.Server.Players);
                            break;
                        case KeywordType.TIME:
                            replacer = DateTime.Now.ToString("T");
                            break;
                        case KeywordType.DATE:
                            replacer = DateTime.Now.ToString("d");
                            break;
                        case KeywordType.WORLDTIME12:
                            /*if (user != null)
                            {
                                replacer = DescParseTickFormat.format12(user.getWorld() == null ? 0 : user.getWorld().getTime());
                            }*/
                            break;
                        case KeywordType.WORLDTIME24:
                            /*if (user != null)
                            {
                                replacer = DescParseTickFormat.format24(user.getWorld() == null ? 0 : user.getWorld().getTime());
                            }*/
                            break;
                        case KeywordType.WORLDDATE:
                            /*if (user != null)
                            {
                                replacer = DateFormat.getDateInstance(DateFormat.MEDIUM, ess.getI18n().getCurrentLocale()).format(DescParseTickFormat.ticksToDate(user.getWorld() == null ? 0 : user.getWorld().getFullTime()));
                            }*/
                            break;
                        case KeywordType.COORDS:
                            /*if (user != null)
                            {
                                Location location = user.getLocation();
                                replacer = tl("coordsKeyword", location.getBlockX(), location.getBlockY(), location.getBlockZ());
                            }*/
                            break;
                        case KeywordType.TPS:
                            replacer = $"{Globals.Server.TPS}";
                            break;
                        case KeywordType.UPTIME:
                            replacer = Globals.Server.StartTime.ToString("g");
                            break;
                        case KeywordType.IP:
                            /*if (user != null)
                            {
                                replacer = user.getBase().getAddress() == null || user.getBase().getAddress().getAddress() == null ? "" : user.getBase().getAddress().getAddress().tostring();
                            }*/
                            break;
                        case KeywordType.ADDRESS:
                            /*if (user != null)
                            {
                                replacer = user.getBase().getAddress() == null ? "" : user.getBase().getAddress().tostring();
                            }*/
                            break;
                        case KeywordType.PLUGINS:
                            /*StringBuilder pluginlistBuilder = new StringBuilder();
                            foreach (IPluginInfo p in Globals.Server.)
                            {
                                if (pluginlistBuilder.Length > 0)
                                {
                                    pluginlistBuilder.Append(", ");
                                }
                                pluginlistBuilder.Append(p.Name);
                            }
                            replacer = pluginlistBuilder.tostring();*/
                            break;
                        case KeywordType.VERSION:
                            replacer = Globals.Server.Version;
                            break;
                        default:
                            replacer = "N/A";
                            break;
                    }

                    /*if (replaceSpacesWithUnderscores)
                    {
                        replacer = replacer.Replace("\\s", "_");
                    }

                    //If this is just a regular keyword, lets throw it into the cache
                    if (validKeyword.GetType().Equals(KeywordCachable.CACHEABLE))
                    {
                        keywordCache.put(validKeyword, replacer);
                    }*/
                }

                //line = line.Replace(fullMatch, replacer);
            }
            catch (Exception) {
            }
            return keyword;
        }
        #endregion
    }
}
