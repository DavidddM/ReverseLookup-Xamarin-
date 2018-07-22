using System.Collections.Generic;

using Newtonsoft.Json;

namespace ReverseLookup
{
    class APIResponse
    {
        [JsonProperty(PropertyName = "res")]
        private string Response { get; set; }

        [JsonProperty(PropertyName = "items")]
        public List<string> Items { get; set; }

        public bool Success => Response.Equals("yes");
    }
}