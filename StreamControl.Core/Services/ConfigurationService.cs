using Newtonsoft.Json;
using System.IO;

namespace StreamControl.Core
{
    public class ConfigurationService
    {
        public static ConfigurationService Load(string path)
        {
            return JsonConvert.DeserializeObject<ConfigurationService>(File.ReadAllText(path));
        }

    }
}
