using System.Collections.Generic;

namespace WebTagger.Jobs
{
    public interface IJobRepository
    {
        ICollection<Job> Get();
        void RegisterAdhocJob(string url, string jobName);
    }
}