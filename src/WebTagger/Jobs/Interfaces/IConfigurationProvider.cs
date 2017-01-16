using System;
using System.Collections.Generic;

namespace WebTagger.Jobs.Configuration
{
    public interface IConfigurationProvider
    {
        TimeSpan DelayBetweenJobCycle { get;  }
        void AddConfigFile(string filename);
        ICollection<Job> GetJobs();
    }
}