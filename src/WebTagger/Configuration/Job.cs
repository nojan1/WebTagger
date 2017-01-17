using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Configuration
{ 
    public enum OutputType
    {
        [JsonProperty("tag")]
        Tag,
        [JsonProperty("url")]
        Url
    }

    public class Selection
    {
        [JsonProperty("output", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OutputType Output { get; set; }

        [JsonProperty("searchpath", Required = Required.Always)]
        [MinLength(1)]
        public string SearchPath { get; set; }

        [JsonProperty("tagname", Required = Required.DisallowNull)]
        public string TagName { get; set; }

        [JsonProperty("hardcoded", Required = Required.DisallowNull)]
        public bool Hardcoded { get; set; }

        [JsonProperty("value", Required = Required.DisallowNull)]
        public string Value { get; set; }

        [JsonProperty("jobname", Required = Required.DisallowNull)]
        public string JobName { get; set; }
    }

    public class Job
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("url", Required = Required.DisallowNull)]
        [Url]
        public string Url { get; set; }

        [JsonProperty("replace", Required = Required.DisallowNull)]
        public bool Replace { get; set; }

        [JsonProperty("selections", Required = Required.Always)]
        public List<Selection> Selections { get; set; }
    }
}
