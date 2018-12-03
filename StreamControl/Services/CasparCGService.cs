using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StilSoft.CasparCG.AmcpClient;
using StilSoft.CasparCG.AmcpClient.Commands;

namespace StreamControl
{
    public class CasparCGService : ICasparCGService
    {
        private AmcpConnection connection;

        public async Task Connect()
        {
            connection = new AmcpConnection() { AutoConnect = true, AutoReconnect = true, ReconnectAttempts = 5, KeepAliveEnable = true };
            await connection.ConnectAsync();
        }

        public async Task<bool> SendCommandAsync(string command)
        {
            try
            {
                await new CustomCommand(command).ExecuteAsync(connection);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SendCommandsAsync(IEnumerable<string> commands)
        {
            bool result = true;
            foreach (var item in commands)
                if (!await SendCommandAsync(item))
                    result = false;
            return result;
        }
    }
}
