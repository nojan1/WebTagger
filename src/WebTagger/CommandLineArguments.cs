using CommandLineParser.Arguments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTagger
{
    public class CommandLineArguments
    {
        [SwitchArgument('d', false, LongName = "daemon", Description = "Run continously in background")]
        public bool background;

        [ValueArgument(typeof(string), 'c', LongName = "config", AllowMultiple = true, Description = "Configuration file to use. Multiple files can be specified")]
        public string[] configurationFiles;
    }
}
