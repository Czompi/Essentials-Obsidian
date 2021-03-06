﻿using System;
using System.Text.Json.Serialization;

namespace Essentials.Configs
{
    public class WarpConfig
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("world")]
        public string World { get; set; }
        [JsonPropertyName("position")]
        public Position Position { get; set; }
        [JsonPropertyName("yaw")]
        public double Yaw { get; set; } = 0.0;
        [JsonPropertyName("pitch")]
        public double Pitch { get; set; } = 0.0;
        [JsonPropertyName("lastowner")]
        public Guid LastOwner { get; set; }
    }
}