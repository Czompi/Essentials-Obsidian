using System.Text.Json;
using System.Text.Json.Serialization;

namespace Essentials.Configs
{
    public class Home
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("world")]
        public string World { get; set; }
        [JsonPropertyName("position")]
        public Position Position { get; set; }
    }
}
