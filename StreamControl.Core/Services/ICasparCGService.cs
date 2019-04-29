using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamControl.Core.Services
{
    public interface ICasparCGService
    {
        Task Connect();
        Task<bool> SendCommandAsync(string command);
        Task<bool> SendCommandsAsync(IEnumerable<string> commands);
    }
}