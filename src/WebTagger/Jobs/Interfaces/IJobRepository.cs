using System.Collections.Generic;
using WebTagger.Jobs.Configuration;

namespace WebTagger.Jobs
{
    public interface IJobRepository
    {
        ICollection<Job> Get();
        void RegisterAdhocJob(string url, string jobName);
    }
}