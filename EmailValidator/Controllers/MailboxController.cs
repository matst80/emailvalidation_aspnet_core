using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmailValidator.Controllers
{
    [Route("api/[controller]")]
    public class MailboxController : Controller
    {

        [HttpGet("{ip}/{email}")]
        public MailBoxResult Validate(string ip, string email)
        {
            var ret = new MailBoxResult();
            ret.Validate(ip,email);
            return ret;
        }


    }
}
