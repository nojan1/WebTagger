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

        public ICollection<Job> Get()
        {
            var jobs = configurationProvider.GetJobs().ToList();
            jobs.AddRange(AdHocJobs);

            AdHocJobs.Clear();

            return jobs;
        }

        public void RegisterAdhocJob(string url, string jobName)
        {
            var targetJob = configurationProvider.GetJobs()
                                                 .FirstOrDefault(j => j.Name == jobName);

            if (targetJob == null)
                throw new Exception("No such job " + jobName);

            targetJob.Url = url;
            AdHocJobs.Add(targetJob);
        }
    }
}
