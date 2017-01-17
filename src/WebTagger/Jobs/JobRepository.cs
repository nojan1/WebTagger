using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTagger.Configuration;

namespace WebTagger.Jobs
{
    public class JobRepository : IJobRepository
    {
        private List<Job> AdHocJobs = new List<Job>();

        private readonly IConfigurationProvider configurationProvider;

        public JobRepository(IConfigurationProvider configurationProvider)
        {
            this.configurationProvider = configurationProvider;
        }

        public ICollection<Job> GetConfiguredJobs()
        {
            return configurationProvider.GetJobs().ToList();
        }

        public ICollection<Job> GetAdhocJobs()
        {
            var copy = AdHocJobs.ToList();
            AdHocJobs.Clear();

            return copy;
        }

        public void RegisterAdhocJob(string url, string jobName)
        {
            var targetJob = configurationProvider.GetJobs()
                                                 .FirstOrDefault(j => j.Name == jobName);

            if (targetJob == null)
                throw new Exception("No such job " + jobName);

            AdHocJobs.Add(new Job
            {
                Url = url,
                Name = targetJob.Name,
                Replace = targetJob.Replace,
                Selections = targetJob.Selections.Select(s => new Selection
                {
                    Hardcoded = s.Hardcoded,
                    JobName = s.JobName,
                    Output = s.Output,
                    SearchPath = s.SearchPath,
                    TagName = s.TagName,
                    Value = s.Value
                }).ToList()
            });
        }
    }
}
