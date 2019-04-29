using StilSoft.CasparCG.AmcpClient;
using StilSoft.CasparCG.AmcpClient.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StreamControl.Core.Services
{
    public class CasparCGService : ICasparCGService
    {
        private AmcpConnection connection;

        public CasparCGService()
        {
            connection = new AmcpConnection() { AutoConnect = true, AutoReconnect = true, ReconnectAttempts = 5, KeepAliveEnable = true };
        }

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
            if (commands == null)
                return true;

            bool result = true;
            foreach (var item in commands)
                if (!await SendCommandAsync(item))
                    result = false;
            return result;
        }
    }
}
