using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamControl
{
    public interface ICasparCGService
    {
        Task Connect();
        Task<bool> SendCommandAsync(string command);
        Task<bool> SendCommandsAsync(IEnumerable<string> commands);
    }
}