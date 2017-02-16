using System;
using System.Net;

namespace EmailValidator.Controllers
{
    public class MailBoxResult
    {
        internal void Validate(string ip, string email)
        {
            var ipAddr = IPAddress.Parse(ip);

        }
    }
}