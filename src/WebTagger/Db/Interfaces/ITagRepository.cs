using System.Collections.Generic;

namespace WebTagger.Db
{
    public interface ITagRepository
    {
        void AddTag(string url, string name, string value);
        void Clear(string url);
        ICollection<Tag> List();
        ICollection<Tag> SearchTags(string searchPhrase);
    }
}