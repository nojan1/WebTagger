using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebTagger.Configuration;
using WebTagger.Db;
using WebTagger.Jobs;

namespace WebTagger.Webparsing
{
    public class JobProcessor
    {
        private static ILog logger = LogManager.GetLogger(typeof(JobProcessor)); 

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

            try
            {
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
            catch (Exception ex)
            {
                logger.Error($"Job '{job.Name}' failed", ex);
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

                if (background)
                {
                    Task.Delay(configurationProvider.DelayBetweenJobCycle).Wait();
                }

            } while (background);
        }
    }
}
