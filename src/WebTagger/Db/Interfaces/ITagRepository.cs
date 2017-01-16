namespace WebTagger.Db
{
    public interface ITagRepository
    {
        void AddOrUpdateTag(string url, string name, string value, bool update);
    }
}