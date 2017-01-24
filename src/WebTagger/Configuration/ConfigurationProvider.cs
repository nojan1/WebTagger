using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private List<Job> configJobs = new List<Job>();
        private List<Client> clients = new List<Client>();

        public ConfigurationProvider()
        {
            DelayBetweenJobCycle = TimeSpan.FromHours(1); //Default
        }

        public TimeSpan DelayBetweenJobCycle
        {
            get; private set;
        }

        public string QueryServiceListenUrl { get; private set; }

        public string LogConfigFilePath { get; private set; }

        public string ConnectionString { get; private set; }

        public void AddConfigFile(string filename)
        {
            var configJson = File.ReadAllText(filename);
            var config = JObject.Parse(configJson);

            var generator = new JSchemaGenerator();
            generator.GenerationProviders.Add(new StringEnumGenerationProvider { CamelCaseText = true } );
            var schema = generator.Generate(typeof(ConfigurationFileModel));

            try
            {
                config.Validate(schema);
            }
            catch (JSchemaException ex)
            {
                throw new BadConfigurationException(ex);
            }

            var model = JsonConvert.DeserializeObject<ConfigurationFileModel>(configJson);

            if(model.Jobs != null)
                configJobs.AddRange(model.Jobs);

            if (model.Clients != null)
                clients.AddRange(model.Clients);

            if (!string.IsNullOrWhiteSpace(model.Interval))
            {
                DelayBetweenJobCycle = ConvertToTimeSpan(model.Interval);
            }

            if (!string.IsNullOrWhiteSpace(model.ConnectionString))
            {
                ConnectionString = model.ConnectionString;
            }

            if (!string.IsNullOrWhiteSpace(model.LogConfigFilePath))
            {
                LogConfigFilePath = model.LogConfigFilePath;
            }

            if (!string.IsNullOrWhiteSpace(model.QueryServiceListenUrl))
            {
                QueryServiceListenUrl = model.QueryServiceListenUrl;
            }
        }

        public ICollection<Job> GetJobs()
        {
            return configJobs.ToList();
        }

        public ICollection<Client> GetClients()
        {
            return clients.ToList();
        }

        private static TimeSpan ConvertToTimeSpan( string timeSpan)
        {
            var l = timeSpan.Length - 1;
            var value = timeSpan.Substring(0, l);
            var type = timeSpan.Substring(l, 1);

            switch (type)
            {
                case "d": return TimeSpan.FromDays(double.Parse(value));
                case "h": return TimeSpan.FromHours(double.Parse(value));
                case "m": return TimeSpan.FromMinutes(double.Parse(value));
                case "s": return TimeSpan.FromSeconds(double.Parse(value));
                case "f": return TimeSpan.FromMilliseconds(double.Parse(value));
                case "z": return TimeSpan.FromTicks(long.Parse(value));
                default: return TimeSpan.FromDays(double.Parse(value));
            }
        }

    }

    public class BadConfigurationException : Exception
    {
        public BadConfigurationException(Exception innerException) : base(innerException.Message, innerException) { }
    }
}
