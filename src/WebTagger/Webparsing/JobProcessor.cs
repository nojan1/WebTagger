using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebTagger.Jobs;

namespace WebTagger.Webparsing
{
    public class JobProcessor
    {
        

        private readonly ITagRepository tagRepository;
        private readonly IJobRepository jobRepository;
        private readonly IHttpWrapper httpWrapper;

        public JobProcessor(ITagRepository tagRepository,
                            IJobRepository jobRepository,
                            IHttpWrapper httpWrapper)
        {
            this.tagRepository = tagRepository;
            this.jobRepository = jobRepository;
            this.httpWrapper = httpWrapper;
        }

        public async Task ProcessJob(Job job)
        {
            if (string.IsNullOrEmpty(job.url))
            {
                return;
            }

            var html = await httpWrapper.GetPageContent(job.url);
            var pageParser = new Pageparser(html);

            foreach (var selection in job.selections)
            {
                List<string> values;

                if (selection.hardcoded)
                {
                    values = selection.value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
                }
                else
                {
                    values = pageParser.GetSelectionValues(selection.searchpath);
                }

                if (!values.Any())
                {
                    continue;
                }

                foreach (var value in values)
                {
                    if (selection.output == OutputType.Tag)
                    {
                        tagRepository.AddOrUpdateTag(job.url, selection.tagname, value, job.replace);
                    }
                    else
                    {
                        jobRepository.RegisterAdhocJob(value, selection.jobName);
                    }
                }
            }
        }
    }
}
