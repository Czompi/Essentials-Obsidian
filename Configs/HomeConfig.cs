using Newtonsoft.Json;
using Obsidian.Util.DataTypes;
using Obsidian.WorldData;
using System.Collections.Generic;

namespace Essentials.Configs
{
    public class HomeConfig
    {
        [JsonProperty("homes")]
        public List<Home> Homes { get; set; }
    }

    public class Home
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("world")]
        public string World { get; set; }
        [JsonProperty("position")]
        public Position Position { get; set; }
    }
}