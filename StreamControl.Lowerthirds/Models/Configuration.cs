using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamControl.Lowerthirds.Models
{
    public class Configuration
    {
        public ICollection<Lowerthird> Lowerthirds { get; set; }
        public ICollection<string> LowerthirdsActivateCommands { get; set; }
        public ICollection<string> LowerthirdsChangeCommands { get; set; }
        public ICollection<string> LowerthirdsDeactivateCommands { get; set; }
        public string LowerthirdsHeader { get; set; }
        public string LowerthirdDeactivateText { get; set; }
    }
}
