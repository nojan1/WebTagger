using System.Collections.Generic;

namespace WebTagger.Db
{
    public interface ISiteRepository
    {
        ICollection<Site> List(int accessLevel);
    }
}