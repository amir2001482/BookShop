using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FileController : Controller
    {
        private readonly IHostingEnvironment _env;
        public FileController(IHostingEnvironment env)
        {
            _env = env;
        }
       
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        [Route("Upload")] //for ajax upload
        public async Task<IActionResult> Upload(IEnumerable<IFormFile> files)
        {
            foreach(var item in files)
            {
                var FileRoot = Path.Combine(_env.WebRootPath, "GalleryFiles");
                if (!Directory.Exists(FileRoot))
                    Directory.CreateDirectory(FileRoot);
                var FileExtention = Path.GetExtension(item.FileName);
                var FileName = string.Concat(Guid.NewGuid().ToString(), FileExtention);
                var FilePath = Path.Combine(FileRoot, FileName);
                using(var stream = new FileStream(FilePath , FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }
               

            }
            //ViewBag.Alert = "آپلود فایل با موفقیت انجام شد .";
            //return View(); 

            return new JsonResult("Success"); // for ajax upload if dont want ajax comment this and uncomment past comment.

        }
    }
}
