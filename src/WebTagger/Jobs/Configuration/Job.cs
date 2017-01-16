using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Jobs.Configuration
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
        public OutputType Output { get; set; }

        [JsonProperty("searchpath", Required = Required.Always)]
        [MinLength(1)]
        public string SearchPath { get; set; }

        [JsonProperty("tagname")]
        public string TagName { get; set; }

        [JsonProperty("hardcoded")]
        public bool Hardcoded { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("jobname")]
        public string JobName { get; set; }
    }

    public class Job
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        [Url]
        public string Url { get; set; }

        [JsonProperty("replace")]
        public bool Replace { get; set; }

        [JsonProperty("selections")]
        public List<Selection> Selections { get; set; }
    }
}
