using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger.Jobs.Configuration
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private List<Job> configJobs = new List<Job>();

        public ConfigurationProvider()
        {
            DelayBetweenJobCycle = TimeSpan.FromHours(1); //Default
        }

        public TimeSpan DelayBetweenJobCycle
        {
            get; private set;
        }

        public void AddConfigFile(string filename)
        {
            var configJson = File.ReadAllText(filename);
            //var config = JObject.Parse(configJson);

            var model = JsonConvert.DeserializeObject<ConfigurationFileModel>(configJson);
            configJobs.AddRange(model.Jobs);

            //try
            //{

            //}
            //catch(Exception ex)
            //{
            //    var ex2 = new BadConfigurationException();

            //    throw ex2;
            //}

            //if (IsValid(config))
            //{
            //    var model = config.ToObject<ConfigurationFileModel>();

            //    configJobs.AddRange(model.Jobs);
            //}
            //else
            //{

            //}
        }

        public ICollection<Job> GetJobs()
        {
            return configJobs.ToList();
        }

        //private bool IsValid(JObject configJson)
        //{
        //    //var schemaRaw = File.ReadAllText("Jobs/Configuration/configuration-schema.json");
        //    //var schema = JsonSchema.Parse(schemaRaw);

        //    var generator = new JsonSchemaGenerator();
        //    var schema = .Generate(typeof(ConfigurationFileModel));
        //    return configJson.IsValid(schema);
        //}
    }

    public class BadConfigurationException : Exception { }
}
