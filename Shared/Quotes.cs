using Newtonsoft.Json;
using System;

namespace Shared
{
    public class Quotes
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "quote")]
        public string Quote { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }
    }
}
