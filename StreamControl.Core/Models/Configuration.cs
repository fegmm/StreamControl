using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamControl.Core.Models
{
    class Configuration
    {
        public string CancelText { get; set; }
        public string SaveText { get; set; }
        public ICollection<string> InitCommands { get; set; }
    }
}
