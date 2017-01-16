using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebTagger.Jobs;
using WebTagger.Jobs.Configuration;

namespace WebTagger.Webparsing
{
    public class JobProcessor
    {
        

        private readonly ITagRepository tagRepository;
        private readonly IJobRepository jobRepository;
        private readonly IHttpWrapper httpWrapper;
        private readonly IConfigurationProvider configurationProvider;

        public JobProcessor(ITagRepository tagRepository,
                            IJobRepository jobRepository,
                            IHttpWrapper httpWrapper,
                            IConfigurationProvider configurationProvider)
        {
            this.tagRepository = tagRepository;
            this.jobRepository = jobRepository;
            this.httpWrapper = httpWrapper;
            this.configurationProvider = configurationProvider;
        }

        public async Task ProcessJob(Job job)
        {
            if (string.IsNullOrEmpty(job.Url))
            {
                return;
            }

            var html = await httpWrapper.GetPageContent(job.Url);
            var pageParser = new Pageparser(html);

            foreach (var selection in job.Selections)
            {
                List<string> values;

                if (selection.Hardcoded)
                {
                    values = selection.Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
                }
                else
                {
                    values = pageParser.GetSelectionValues(selection.SearchPath);
                }

                if (!values.Any())
                {
                    continue;
                }

                foreach (var value in values)
                {
                    if (selection.Output == OutputType.Tag)
                    {
                        tagRepository.AddOrUpdateTag(job.Url, selection.TagName, value, job.Replace);
                    }
                    else
                    {
                        jobRepository.RegisterAdhocJob(value, selection.JobName);
                    }
                }
            }
        }

        public void ProcessAllJobs(bool background)
        {
            do
            {
                foreach (var job in jobRepository.Get())
                {
                    ProcessJob(job).Wait();
                }



            } while (background);
        }
    }
}
