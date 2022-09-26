using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.ViewModels
{
    public class UploadLargeFileViewModel
    {
        public IFormFile File { get; set; }
    }

    public class UploadFileResult
    {
        public UploadFileResult()
        {

        }
        public UploadFileResult(bool isSuccess , List<string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }
        public bool? IsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}
