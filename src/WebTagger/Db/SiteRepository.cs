using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Db
{
    public class SiteRepository : ISiteRepository
    {
        private readonly IDbContextProvider contextProvider;

        public SiteRepository(IDbContextProvider contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public ICollection<Site> List()
        {
            using(var context = contextProvider.GetContext())
            {
                return context.Sites.ToList();
            }
        }
    }
}
