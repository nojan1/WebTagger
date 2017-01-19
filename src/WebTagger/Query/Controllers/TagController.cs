using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Db;

namespace WebTagger.Query.Controllers
{
    [Route("api/tags")]
    public class TagController : Controller
    {
        private readonly IDbContextProvider dbContextProvider;

        public TagController(IDbContextProvider dbContextProvider)
        {
            this.dbContextProvider = dbContextProvider;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            using(var context = dbContextProvider.GetContext())
            {
                return context.Tags.Select(t => $"{t.Name}=>{t.Value}").ToList();
            }
        }
    }
}
