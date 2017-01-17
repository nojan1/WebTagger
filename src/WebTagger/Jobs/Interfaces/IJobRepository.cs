using System.Collections.Generic;
using WebTagger.Configuration;

namespace WebTagger.Jobs
{
    public interface IJobRepository
    {
        ICollection<Job> GetConfiguredJobs();
        ICollection<Job> GetAdhocJobs();
        void RegisterAdhocJob(string url, string jobName);
    }
}