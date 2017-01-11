using System.Threading.Tasks;

namespace WebTagger.Webparsing
{
    public interface IHttpWrapper
    {
        Task<string> GetPageContent(string url);
    }
}