using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Api.Controllers.V2
{
    [Route("api/[controller]")]
    //[Route("api/{V:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]

    public class SampleV2Controller : ControllerBase
    {
        [HttpGet]
        public List<string> HelloWorld()
        {
            var res = new List<string>()
            {
                "value 1 from sample 2" ,
                "value 2 from sample 2"
            };
            return res;
        }
    }
}
