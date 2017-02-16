using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EmailValidator.Controllers
{
    public class MailBoxResult
    {
        public bool Exists { get; set; }
        public bool ServerAnswer { get; set; }

        internal void Send(string data, Socket s)
        {
            var sendData = Encoding.UTF8.GetBytes(data+"\r\n");
            s.Send(sendData);
        }

        internal string GetString(Socket s)
        {
            var resBuffer = new byte[4096];
            s.Receive(resBuffer);
            return Encoding.UTF8.GetString(resBuffer)?.Trim().TrimEnd('\0');
        }

        internal void Validate(string ip, string email)
        {
            var ipAddr = IPAddress.Parse(ip);
            var endPoint = new IPEndPoint(ipAddr, 25);
            Socket s = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            s.Connect(endPoint);
            var hello = GetString(s);
            Send("helo hi",s);

            var res = GetString(s);
            
            if (res.Length>0)
            {
                ServerAnswer = true;
                Send("mail from: <mailboxcheck@cavagent.com>", s);
                var fromAnswer = GetString(s);
                Send("rcpt to: <"+email+">", s);
                var result = GetString(s);
                if (result.Contains(" OK") || result.Contains("250"))
                    Exists = true;
            }
            Send("quit", s);

        }
    }
}