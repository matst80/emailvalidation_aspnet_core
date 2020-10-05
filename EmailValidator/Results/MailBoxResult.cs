using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EmailValidator.Controllers
{
    public class MailBoxResult
    {
        public bool Exists { get; private set; }
        public bool ServerAnswer { get; private set; }

        private void Send(string data, Socket socket)
        {
            var sendData = Encoding.UTF8.GetBytes(data+"\r\n");
            socket.Send(sendData);
        }

        private string GetString(Socket socket)
        {
            var resBuffer = new byte[4096];
            socket.Receive(resBuffer);
            return Encoding.UTF8.GetString(resBuffer)?.Trim().TrimEnd('\0');
        }

        public void Validate(string ip, string email)
        {
            var ipAddr = IPAddress.Parse(ip);
            var endPoint = new IPEndPoint(ipAddr, 25);
            var socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(endPoint);
            
            Send("helo hi",socket);

            var res = GetString(socket);
            
            if (res.Length>0)
            {
                ServerAnswer = true;
                Send("mail from: <mailboxcheck@cavagent.com>", socket);
                var fromAnswer = GetString(socket);
                Send("rcpt to: <"+email+">", socket);
                var result = GetString(socket);
                if (result.Contains(" OK") || result.Contains("250"))
                    Exists = true;
            }
            Send("quit", socket);

        }
    }
}