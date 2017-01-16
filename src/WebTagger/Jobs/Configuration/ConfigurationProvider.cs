using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Jobs.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public void AddConfigFile(string filename)
        {

        }

        public ICollection<Job> GetJobs()
        {
            return new List<Job>();
        }
    }
}
