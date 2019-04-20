using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamControl.Streams.Models
{
    public class Configuration
    {
        public string StreamsHeader { get; set; }
        public ICollection<Models.Stream> Streams { get; set; }
    }
}
