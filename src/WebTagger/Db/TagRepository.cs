using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Jobs;
using WebTagger.Query.Model;

namespace WebTagger.Db
{
    public class TagRepository : ITagRepository
    {
        private readonly IDbContextProvider contextProvider;

        public TagRepository(IDbContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public void AddTag(string url, string name, string value, int accessLevel)
        {
            using (var context = contextProvider.GetContext())
            {
                var site = context.Sites.Include(s => s.Tags).FirstOrDefault(s => s.Url == url);
                if (site == null)
                {
                    site = new Site
                    {
                        Url = url,
                        Tags = new List<Tag>(),
                        AccessLevel = accessLevel
                    };

                    context.Sites.Add(site);
                }
                else if (site.AccessLevel < accessLevel)
                {
                    site.AccessLevel = accessLevel;
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

        public ICollection<Tag> List(int accessLevel)
        {
            using (var context = contextProvider.GetContext())
            {
                return context.Tags.Include(t => t.Site)
                                   .Where(t => t.Site.AccessLevel <= accessLevel)
                                   .ToList();
            }
        }

        //TODO: Write tests for access level limitation
        public ICollection<Tag> SearchTags(string searchPhrase, int accessLevel)
        {
            var searchModel = CreateSearchModel(searchPhrase);

            using (var context = contextProvider.GetContext())
            {
                return context.Tags.Include(t => t.Site)
                                   .Where(t => t.Site.AccessLevel <= accessLevel)
                                   .Where(t => SiteIsMatch(t.Site, searchModel))
                                   .Where(t => TagIsMatch(t, searchModel))
                                   .ToList();
            }
        }

        private SearchModel CreateSearchModel(string searchPhrase)
        {
            var searchModel = new SearchModel();
            var terms = searchPhrase.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var keyValueString in terms.Where(t => t.Contains("=")).ToList())
            {
                var key = keyValueString.Substring(0, keyValueString.IndexOf('=')).ToLower();
                var value = keyValueString.Substring(keyValueString.IndexOf('=') + 1).ToLower();

                searchModel.TagRequirments[key] = value;

                terms.RemoveAll(s => s == keyValueString);
            }

            foreach (var not in terms.Where(t => t.StartsWith("-")).ToList())
            {
                searchModel.Not.Add(not.Substring(1).ToLower());

                terms.RemoveAll(s => s == not);
            }

            searchModel.AllOf = terms.Select(t => t.ToLower()).ToList();

            return searchModel;
        }

        private bool SiteIsMatch(Site site, SearchModel searchModel)
        {
            if (searchModel.TagRequirments.ContainsKey("site"))
            {
                var uri = new Uri(site.Url);
                return uri.Host.Contains(searchModel.TagRequirments["site"]);
            }

            return true;
        }

        private bool TagIsMatch(Tag tag, SearchModel searchModel)
        {
            if (searchModel.TagRequirments.ContainsKey(tag.Name.ToLower()) &&
               !tag.Value.ToLower().Contains(searchModel.TagRequirments[tag.Name.ToLower()]))
            {

                return false;
            }

            if (searchModel.Not.Any(n => tag.Value.ToLower().Contains(n)))
            {
                return false;
            }

            return searchModel.AllOf.All(s => tag.Value.ToLower().Contains(s));
        }
    }
}
