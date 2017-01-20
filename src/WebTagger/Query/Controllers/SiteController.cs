using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Db;

namespace WebTagger.Query.Controllers
{
    [Route("api/site")]
    public class SiteController : Controller
    {
        private readonly ISiteRepository siteRepository;

        public SiteController(ISiteRepository siteRepository)
        {
            this.siteRepository = siteRepository;
        }

        [HttpGet]
        public ICollection<Site> Get()
        {
            return siteRepository.List();
        }
    }
}
