using System.Text.Json.Serialization;

namespace Essentials.Configs
{
    public class Position
    {
        [JsonPropertyName("x")]
        public float X { get; set; }
        [JsonPropertyName("y")]
        public float Y { get; set; }
        [JsonPropertyName("z")]
        public float Z { get; set; }
    }
}
