using System.Collections.Generic;

namespace WebTagger.Db
{
    public interface ITagRepository
    {
        void AddTag(string url, string name, string value, int accessLevel);
        void Clear(string url);
        ICollection<Tag> List(int accessLevel);
        ICollection<Tag> SearchTags(string searchPhrase, int accessLevel);
    }
}