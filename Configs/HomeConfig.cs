using Newtonsoft.Json;
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

    public class Position
    {
        [JsonProperty("x")]
        public int X { get; set; }
        [JsonProperty("y")]
        public int Y { get; set; }
        [JsonProperty("z")]
        public int Z { get; set; }
    }
}