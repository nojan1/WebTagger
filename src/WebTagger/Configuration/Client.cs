using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Configuration
{
    public class Client
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("clientid", Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty("clientkey", Required = Required.Always)]
        public string Key { get; set; }

        [JsonProperty("authlevel", Required = Required.Always)]
        public int AuthLevel { get; set; }
    }
}
