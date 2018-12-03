using StreamControl.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamControl
{
    public class ConfigurationService : IConfigurationService
    {
        public string StreamsHeader { get; set; }
        public string LowerthirdsHeader { get; set; }
        public string LowerthirdDeactivateText { get; set; }
        public ICollection<Models.Stream> Streams { get; set; }
        public ICollection<string> Lowerthirds { get; set; }
        public ICollection<string> LowerthirdsActivateCommands { get; set; }
        public ICollection<string> LowerthirdsChangeCommands { get; set; }
        public ICollection<string> LowerthirdsDeactivateCommands { get; set; }
        public ICollection<string> InitCommands { get; set; }

        public static ConfigurationService Load(string path)
        {
            return JsonConvert.DeserializeObject<ConfigurationService>(File.ReadAllText(path));
        }

    }
}
