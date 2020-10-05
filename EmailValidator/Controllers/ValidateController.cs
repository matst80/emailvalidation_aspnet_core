using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EmailValidator.Controllers
{
    
    [Route("api/[controller]")]
    public class ValidateController : Controller
    {

        [HttpGet("{email}")]
        public async Task<ValidationResult> Validate(string email)
        {
            var ret = new ValidationResult();
            await ret.Validate(email);
            return ret;
        }


    }
}
