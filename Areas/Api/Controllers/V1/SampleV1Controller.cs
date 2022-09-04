using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("3.0")]
    public class SampleV1Controller : ControllerBase
    {
        [HttpGet]
        public List<string> HelloWorld()
        {
            var res = new List<string>()
            {
                "value 1 from sample 1" ,
                "value 2 from sample 1"
            };
            return res;
        }

        [HttpGet("{name}") , MapToApiVersion("3.0")]
        public string GetName(string name)
        {
            var apiVersion = HttpContext.GetRequestedApiVersion().ToString();
            return name + apiVersion;
        }
    }
  
}
