using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Essentials.Configs
{
    [JsonConverter(typeof(List<Home>))]
    public class HomeConfig : List<Home> { }
}