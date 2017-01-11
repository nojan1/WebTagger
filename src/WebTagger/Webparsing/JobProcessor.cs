using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebTagger.Jobs;

namespace WebTagger.Webparsing
{
    public class JobProcessor
    {
        private static HttpClient httpClient = new HttpClient();

        public JobProcessor()
        {
            
        }

        public async Task ProcessJob(Job job)
        {
            if (string.IsNullOrEmpty(job.url))
            {
                return;
            }

            var html = await httpClient.GetStringAsync(job.url);
            var pageParser = new Pageparser(html);
                        
            foreach(var selection in job.selections)
            {
                List<string> values;

                if (selection.hardcoded)
                {
                    values = selection.value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
                }
                else
                {
                    values = pageParser.GetSelectionValues(selection.searchpath);
                }

                if (!values.Any())
                {
                    continue;
                }

                foreach(var value in values)
                {
                    if(selection.output == OutputType.Tag)
                    {

                    }
                    else
                    {

                    }
                }
            }
        }
    }
}
