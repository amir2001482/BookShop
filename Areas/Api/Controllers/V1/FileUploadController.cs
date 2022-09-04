using BookShop.Areas.Api.Class;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FileUploadController : ControllerBase
    {
        private readonly IHostingEnvironment _ev;
        public FileUploadController(IHostingEnvironment ev)
        {
            _ev = ev;
        }
        [HttpPost]
        public async Task<ApiResult<string>> UploadeImage([FromBody]string ImageBase64)
        {
            byte[] Bytes = Convert.FromBase64String(ImageBase64);
            var FilePath = Path.Combine($"{_ev.WebRootPath}/Files/{Guid.NewGuid()}.jpg");
            await System.IO.File.WriteAllBytesAsync(FilePath, Bytes);
            return Ok("آپلود عکس با موفقیت انجام شد");
        }
    }
}
