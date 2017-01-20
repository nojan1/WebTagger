using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Db;
using WebTagger.Query.Model;

namespace WebTagger.Query.Controllers
{
    [Route("api/tags")]
    public class TagController : Controller
    {
        private readonly ITagRepository tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IEnumerable<TagModel> Get()
        {
            return tagRepository.List()
                                .Select(t => new TagModel { Name = t.Name, Value = t.Value })
                                .ToList();
        }

        [HttpGet("search")]
        public IEnumerable<SiteInfoWithTagsModel> Search(string q)
        {
            return tagRepository.SearchTags(q ?? "")
                                .GroupBy(t => t.Site.Url)
                                .Select(x => new SiteInfoWithTagsModel
                                {
                                    URL = x.Key,
                                    Tags = x.Select(t => new TagModel { Name = t.Name, Value = t.Value }).ToList()
                                }).ToList();
        }
    }
}
