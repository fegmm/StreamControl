using System.Collections.Generic;

namespace StreamControl.Startup.Models
{
    public class StartupConfiguration
    {
        public Launchprogram[] LaunchPrograms { get; set; }
        public Configuration[] Configurations { get; set; }
        public string[] InitCommands { get; set; }
        public Dictionary<string,string> Defaults { get; set; }
    }

}
