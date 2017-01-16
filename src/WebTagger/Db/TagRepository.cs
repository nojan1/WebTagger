using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Jobs;

namespace WebTagger.Db
{
    public class TagRepository : ITagRepository
    {
        private readonly IDbContextProvider contextProvider;

        public TagRepository(IDbContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public void AddOrUpdateTag(string url, string name, string value, bool update)
        {
            using (var context = contextProvider.GetContext())
            {
                var site = context.Sites.Include(s => s.Tags).FirstOrDefault(s => s.Url == url);
                if (site == null)
                {
                    site = new Site
                    {
                        Url = url,
                        Tags = new List<Tag>()
                    };

                    context.Sites.Add(site);
                }

                var tag = site.Tags.FirstOrDefault(t => t.Name == name);
                if (tag == null || !update)
                {
                    site.Tags.Add(new Tag
                    {
                        Name = name,
                        Site = site,
                        Value = value
                    });
                }
                else
                {
                    tag.Value = value;
                }

                context.SaveChanges();
            }
        }
    }
}
