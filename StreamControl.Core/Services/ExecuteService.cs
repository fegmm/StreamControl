using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamControl.Core.Services
{
    public class ExecuteService : IExecuteService
    {
        public void ExecuteCommandLine(string command)
        {
            RunProcess("cmd.exe", "/C" + command, ProcessWindowStyle.Hidden, true);
        }

        public void RunProcess(string path, string arguments = null, ProcessWindowStyle windowStyle = ProcessWindowStyle.Normal, bool errorDialog = true)
        {
            var startInfo = new ProcessStartInfo(path) { WindowStyle = windowStyle, ErrorDialog = errorDialog };
            if (arguments != null)
                startInfo.Arguments = arguments;
            var process = Process.Start(startInfo);
        }
    }
}
