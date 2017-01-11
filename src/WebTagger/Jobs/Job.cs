using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Jobs
{ 
    public enum OutputType
    {
        Tag,
        Url
    }

    public class Selection
    {
        public OutputType output { get; set; }
        public string searchpath { get; set; }
        public string tagname { get; set; }
        public bool hardcoded { get; set; }
        public string value { get; set; }
        public string jobName { get; set; }
    }

    public class Job
    {
        public string name { get; set; }
        public string url { get; set; }
        public bool replace { get; set; }
        public List<Selection> selections { get; set; }
    }
}
