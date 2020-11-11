using Essentials.Configs.GlobalModel;
using System.Collections;
using System.Text.Json.Serialization;

namespace Essentials.Configs
{
    public class GlobalConfig
    {
        [JsonPropertyName("global")]
        public Global Global { get; set; }
        [JsonPropertyName("antibuild")]
        public AntiBuild AntiBuild { get; set; }
        [JsonPropertyName("chat")]
        public Chat Chat { get; set; }
        [JsonPropertyName("economy")]
        public Economy Economy { get; set; }
        [JsonPropertyName("homes")]
        public Homes Homes { get; set; }
        [JsonPropertyName("protect")]
        public AntiBuild Protect { get; set; }
    }
}