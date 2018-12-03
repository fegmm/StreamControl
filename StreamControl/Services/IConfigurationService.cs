using System.Collections.Generic;
using StreamControl.Models;

namespace StreamControl
{
    public interface IConfigurationService
    {
        ICollection<string> InitCommands { get; set; }
        string LowerthirdDeactivateText { get; set; }
        ICollection<string> Lowerthirds { get; set; }
        ICollection<string> LowerthirdsActivateCommands { get; set; }
        ICollection<string> LowerthirdsChangeCommands { get; set; }
        ICollection<string> LowerthirdsDeactivateCommands { get; set; }
        string LowerthirdsHeader { get; set; }
        ICollection<Stream> Streams { get; set; }
        string StreamsHeader { get; set; }
    }
}