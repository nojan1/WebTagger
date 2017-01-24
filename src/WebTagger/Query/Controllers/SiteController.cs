using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Configuration;
using WebTagger.Db;

namespace WebTagger.Query.Controllers
{
    [Route("api/site")]
    public class SiteController : BaseController
    {
        private readonly ISiteRepository siteRepository;

        public SiteController(ISiteRepository siteRepository, IConfigurationProvider configurationProvider) : base(configurationProvider)
        {
            this.siteRepository = siteRepository;
        }

        [HttpGet]
        public ICollection<Site> Get()
        {
            return siteRepository.List(GetAccessLevel());
        }
    }
}
