using System.Diagnostics;

namespace StreamControl.Core.Services
{
    public interface IExecuteService
    {
        void ExecuteCommandLine(string command);
        void RunProcess(string path, string arguments = null, ProcessWindowStyle windowStyle = ProcessWindowStyle.Normal, bool errorDialog = true);
    }
}