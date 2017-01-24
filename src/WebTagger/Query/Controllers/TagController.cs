using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Configuration;
using WebTagger.Db;
using WebTagger.Query.Model;

namespace WebTagger.Query.Controllers
{
    [Route("api/tags")]
    public class TagController : BaseController
    {
        private readonly ITagRepository tagRepository;

        public TagController(ITagRepository tagRepository, IConfigurationProvider configurationProvider) : base(configurationProvider)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IEnumerable<TagModel> Get()
        {
            return tagRepository.List(GetAccessLevel())
                                .Select(t => new TagModel { Name = t.Name, Value = t.Value })
                                .ToList();
        }

        [HttpGet("search")]
        public IEnumerable<SiteInfoWithTagsModel> Search(string q)
        {
            return tagRepository.SearchTags(q ?? "", GetAccessLevel())
                                .GroupBy(t => t.Site.Url)
                                .Select(x => new SiteInfoWithTagsModel
                                {
                                    URL = x.Key,
                                    Tags = x.Select(t => new TagModel { Name = t.Name, Value = t.Value }).ToList()
                                }).ToList();
        }
    }
}
