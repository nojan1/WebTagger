using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Query.Model
{
    public class SearchModel
    {
        public ICollection<string> AllOf { get; set; } = new List<string>();
        public ICollection<string> Not { get; set; } = new List<string>();
        public Dictionary<string, string> TagRequirments { get; set; } = new Dictionary<string, string>();
    }
}
