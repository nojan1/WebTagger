using System.Collections.Generic;

namespace WebTagger.Jobs.Configuration
{
    public interface IConfigurationProvider
    {
        void AddConfigFile(string filename);
        ICollection<Job> GetJobs();
    }
}