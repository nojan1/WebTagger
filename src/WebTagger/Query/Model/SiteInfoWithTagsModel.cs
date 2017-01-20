using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Query.Model
{
    public class SiteInfoWithTagsModel
    {
        public string URL { get; set; }
        public ICollection<TagModel> Tags { get; set; }
    }

    public class TagModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
