using System.Diagnostics;

namespace StreamControl.Startup.Models
{
    public class Launchprogram
    {
        public string Path { get; set; }
        public ProcessWindowStyle WindowStyle { get; set; }
        public string Arguments { get; set; }
    }

}
