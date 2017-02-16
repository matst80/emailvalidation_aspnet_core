using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace EmailValidator.Controllers
{
    
    [Route("api/[controller]")]
    public class ValidateController : Controller
    {

        [HttpGet("{email}")]
        public ValidationResult Validate(string email)
        {
            var ret = new ValidationResult();
            ret.Validate(email);
            return ret;
        }


    }
}
