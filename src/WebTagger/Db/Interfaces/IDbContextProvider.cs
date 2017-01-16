namespace WebTagger.Db
{
    public interface IDbContextProvider
    {
        ApplicationContext GetContext();
    }
}