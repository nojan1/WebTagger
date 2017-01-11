using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Jobs
{
    public class JobRepository : IJobRepository
    {
        public ICollection<Job> Get()
        {
            return new List<Job>();
        }

        public void RegisterAdhocJob(string url, string jobName)
        {

        }
    }
}
