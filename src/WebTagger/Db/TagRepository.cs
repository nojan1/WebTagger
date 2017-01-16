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

        public void AddTag(string url, string name, string value)
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

                site.Tags.Add(new Tag
                {
                    Name = name,
                    Site = site,
                    Value = value
                });

                context.SaveChanges();
            }
        }

        public void Clear(string url)
        {
            using (var context = contextProvider.GetContext())
            {
                var site = context.Sites.Include(s => s.Tags).FirstOrDefault(s => s.Url == url);
                if (site != null)
                {
                    foreach (var tag in site.Tags)
                    {
                        context.Tags.Remove(tag);
                    }

                    site.Tags.Clear();

                    context.SaveChanges();
                }
            }
        }
    }
}
