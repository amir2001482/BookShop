using BookShop.Classes;
using BookShop.Models.ViewModels;
using ImageMagick;
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
            try
            {
                foreach (var item in files)
                {
                    var FileRoot = Path.Combine(_env.WebRootPath, "GalleryFiles");
                    if (!Directory.Exists(FileRoot))
                        Directory.CreateDirectory(FileRoot);
                    var FileExtention = Path.GetExtension(item.FileName);
                    var FileName = string.Concat(Guid.NewGuid().ToString(), FileExtention);
                    var FilePath = Path.Combine(FileRoot, FileName);
                    //using(var stream = new FileStream(FilePath , FileMode.Create))
                    //{
                    //    await item.CopyToAsync(stream);
                    //}

                    using (var memory = new MemoryStream())
                    {
                        await item.CopyToAsync(memory);
                        using (var image = new MagickImage(memory.ToArray()))
                        {
                            image.Resize(image.Width / 2, image.Height / 2);
                            image.Quality = 50;
                            image.Write(FilePath);
                        }
                    }
                    CompressImage(FilePath);



                }
                //ViewBag.Alert = "آپلود فایل با موفقیت انجام شد .";
                //return View(); 

                return new JsonResult("Success"); // for ajax upload if dont want ajax comment this and uncomment past comment.
            }
            catch
            {
                return new EmptyResult();
            }
          

        }

        public  IActionResult ImageProcess()
        {
            var FolderPath = $"{_env.ContentRootPath}/Images";
            using (var Image = new MagickImage(FolderPath + "avatar-1.png")) 
            {
                Image.Resize(300, 300);
                Image.Quality = 50;
                Image.Write(FolderPath + "OutPutImage");
            }
            return View();

        }

        public void CompressImage(string path)
        {
            var Image = new FileInfo(path);
            var optimaizer = new ImageOptimizer();
            optimaizer.Compress(Image);
            Image.Refresh();
        }

        public IActionResult SaveImageToPdf()
        {
            var FileRoot = Path.Combine(_env.WebRootPath, "GalleryFiles");
            using(var image = new MagickImage(FileRoot + "logo-header.png"))
            {
                image.Write(FileRoot + "PdfImage.pdf");
            }

            var stream = new FileStream(FileRoot, FileMode.Open, FileAccess.Read);

            return new FileStreamResult(stream, "application/pdf");
            
                
            
        }

        [HttpGet]
        public IActionResult UploadLargeFile()
        {
            return View();
        }
        [HttpPost , ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadLargeFile(UploadLargeFileViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var FileRoot = Path.Combine(_env.WebRootPath, "LargeFiles");
                if (!Directory.Exists(FileRoot))
                    Directory.CreateDirectory(FileRoot);
              
                var FileExtention = Path.GetExtension(model.File.FileName);
                var types = FileExtentions.FileType.PDF;
               
                using (var memory = new MemoryStream())
                {
                    var result = FileExtentions.IsValidFile(memory.ToArray(), types, FileExtention.Replace(".", " "));
                    if (result)
                    {
                        var FileName = string.Concat(Guid.NewGuid().ToString(), FileExtention);
                        var FilePath = Path.Combine(FileRoot, FileName);
                        using (var strame = new FileStream(FilePath, FileMode.Create))
                        {
                            await model.File.CopyToAsync(strame);
                        }
                    }
                   
                }
            }
            return View();
        }
    }
}
