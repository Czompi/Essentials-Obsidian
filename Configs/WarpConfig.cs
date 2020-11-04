using Newtonsoft.Json;
using System;

namespace Essentials.Configs
{
    public class WarpConfig
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("world")]
        public string World { get; set; }
        [JsonProperty("position")]
        public Position Position { get; set; }
        [JsonProperty("yaw")]
        public double Yaw { get; set; } = 0.0;
        [JsonProperty("pitch")]
        public double Pitch { get; set; } = 0.0;
        [JsonProperty("lastowner")]
        public Guid LastOwner { get; set; }
    }
}