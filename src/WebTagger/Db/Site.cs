using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Db
{
    public class Site
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public int AccessLevel { get; set; }
    }
}
