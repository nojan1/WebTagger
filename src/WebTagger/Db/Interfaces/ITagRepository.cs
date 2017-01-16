namespace WebTagger.Db
{
    public interface ITagRepository
    {
        void AddTag(string url, string name, string value);
        void Clear(string url);
    }
}