﻿using log4net;
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
                logger.Debug($"Job '{job.Name}' has no URL, ignoring");
                return;
            }

            try
            {
                var html = await httpWrapper.GetPageContent(job.Url);
                var pageParser = new Pageparser(html);

                if (job.Replace)
                {
                    tagRepository.Clear(job.Url);
                }

                if (!string.IsNullOrEmpty(job.Category))
                {
                    tagRepository.AddTag(job.Url, "category", job.Category, job.AccessLevel);
                }

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
                        logger.Warn($"Job '{job.Name}' returned no values");
                        continue;
                    }

                    foreach (var value in values)
                    {
                        if (selection.Output == OutputType.Tag)
                        {
                            tagRepository.AddTag(job.Url, selection.TagName, value, job.AccessLevel);
                        }
                        else
                        {
                            string url = value;

                            Uri temp;

                            if (!Uri.TryCreate(url, UriKind.Absolute, out temp))
                            {
                                url = new Uri(new Uri(job.Url), url).ToString();
                            }

                            jobRepository.RegisterAdhocJob(url, selection.JobName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Job '{job.Name}' failed", ex);
            }
        }

        public async Task ProcessAllJobs(bool background)
        {
            logger.Info($"Job processing started. Background work {(background ? "enabled" : "disabled")}");

            do
            {
                var jobList = jobRepository.GetConfiguredJobs();

                while (jobList.Any())
                {
                    foreach (var job in jobList)
                    {
                        await ProcessJob(job);
                    }

                    jobList = jobRepository.GetAdhocJobs();
                }

                if (background)
                {
                    logger.Debug($"Begin delay for {configurationProvider.DelayBetweenJobCycle}");
                    await Task.Delay(configurationProvider.DelayBetweenJobCycle);
                }

            } while (background);
        }
    }
}
