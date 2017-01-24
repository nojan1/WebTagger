using System;
using System.Collections.Generic;

namespace WebTagger.Configuration
{
    public interface IConfigurationProvider
    {
        string QueryServiceListenUrl { get; }
        string LogConfigFilePath { get; }
        TimeSpan DelayBetweenJobCycle { get;  }
        string ConnectionString { get; }
        void AddConfigFile(string filename);
        ICollection<Job> GetJobs();
        ICollection<Client> GetClients();
    }
}