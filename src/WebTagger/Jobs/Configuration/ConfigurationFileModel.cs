using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Jobs.Configuration
{
    public class ConfigurationFileModel
    {
        [JsonProperty("interval")]
        [RegularExpression("[0-9]+[dhms]{1}")]
        public string Interval { get; set; }

        [JsonProperty("jobs")]
        public ICollection<Job> Jobs { get; set; }
    }
}
