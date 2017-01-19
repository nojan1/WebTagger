using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Configuration
{
    public class ConfigurationFileModel
    {
        [JsonProperty("connectionString", Required = Required.DisallowNull)]
        public string ConnectionString { get; set; }

        [JsonProperty("queryservicelistenurl", Required = Required.DisallowNull)]
        public string QueryServiceListenUrl { get; set; }

        [JsonProperty("logconfigfilepath", Required = Required.DisallowNull)]
        public string LogConfigFilePath { get; set; }

        [JsonProperty("interval", Required = Required.DisallowNull)]
        [RegularExpression("[0-9]+[dhms]{1}")]
        public string Interval { get; set; }

        [JsonProperty("jobs", Required = Required.DisallowNull)]
        public ICollection<Job> Jobs { get; set; }
    }
}
