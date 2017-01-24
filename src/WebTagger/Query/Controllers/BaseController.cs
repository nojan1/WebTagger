using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Configuration;

namespace WebTagger.Query.Controllers
{
    public class BaseController : Controller
    {
        private readonly IConfigurationProvider configurationProvider;

        public BaseController(IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;
        }

        protected int GetAccessLevel()
        {
            string clientId = Request.Headers.FirstOrDefault(x => x.Key.ToLower() == "clientid").Value;
            string clientKey = Request.Headers.FirstOrDefault(x => x.Key.ToLower() == "clientkey").Value;

            return configurationProvider.GetClients().FirstOrDefault(c => c.Id == clientId && c.Key == clientKey)?.AuthLevel ?? 1;
        }
    }
}
