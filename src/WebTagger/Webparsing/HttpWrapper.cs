using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebTagger.Webparsing
{
    public class HttpWrapper : IHttpWrapper
    {
        private static HttpClient httpClient = new HttpClient();

        public async Task<string> GetPageContent(string url)
        {
            return await httpClient.GetStringAsync(url);
        }
    }
}
