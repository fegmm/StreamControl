using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamControl.Startup.Models
{

    public class Configuration
    {
        public string Name { get; set; }
        public Dictionary<string,string> Values { get; set; }
    }
}
