using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Essentials
{
    [JsonConverter(typeof(Dictionary<string, string>))]
    public class Language : Dictionary<string, string> { }
}
