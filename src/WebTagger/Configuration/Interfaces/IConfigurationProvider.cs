using System;
using System.Collections.Generic;

namespace WebTagger.Configuration
{
    public interface IConfigurationProvider
    {
        TimeSpan DelayBetweenJobCycle { get;  }
        string ConnectionString { get; }
        void AddConfigFile(string filename);
        ICollection<Job> GetJobs();
    }
}