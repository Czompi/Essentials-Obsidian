
        [Command("plugins", "pl")]
        [CommandInfo("Gets all plugins")]
        public async Task PluginsAsync(ObsidianContext Context)
        {
            var pluginCount = Context.Server.PluginManager.Plugins.Count;
            var message = new ChatMessage
            {
                Text = $"{ChatColor.Reset}List of plugins ({ChatColor.Gold}{pluginCount}{ChatColor.Reset}): ",
            };

            var messages = new List<ChatMessage>();
            
            for (int i = 0; i < pluginCount; i++)
            {
                var pluginContainer = Context.Server.PluginManager.Plugins[i];
                var info = pluginContainer.Info;

                var plugin = new ChatMessage();
                var colorByState = pluginContainer.Loaded || pluginContainer.IsReady ? ChatColor.DarkGreen : ChatColor.DarkRed;
                plugin.Text = colorByState + pluginContainer.Info.Name;

                plugin.HoverEvent = new TextComponent { Action = ETextAction.ShowText, Value = $"{colorByState}{info.Name}{ChatColor.Reset}\nVersion: {colorByState}{info.Version}{ChatColor.Reset}\nAuthor(s): {colorByState}{info.Authors}{ChatColor.Reset}" };
                if (pluginContainer.Info.ProjectUrl != null)
                    plugin.ClickEvent = new TextComponent { Action = ETextAction.OpenUrl, Value = pluginContainer.Info.ProjectUrl.AbsoluteUri };

                messages.Add(plugin);

                messages.Add(new ChatMessage
                {
                    Text = $"{ChatColor.Reset}{(i + 1 < Context.Server.PluginManager.Plugins.Count ? ", " : "")}"
                });
            }
            if (messages.Count > 0)
                message.AddExtra(messages);
            Context.Server.Logger.LogDebug(JsonConvert.SerializeObject(message));
            await Context.Player.SendMessageAsync(message);
        }
