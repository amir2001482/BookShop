using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.Repository
{
    public interface IBooksRepository
    {
        List<TreeViewCategory> GetAllCategories();
        void BindSubCategories(TreeViewCategory category);
        Task<bool> CreateBookAsync(BooksCreateEditViewModel ViewModel);
        Task<bool> EditBookAsync(BooksCreateEditViewModel ViewModel);
        List<BooksIndexViewModel> GetAllBooks(string title, string ISBN, string Language, string Publisher, string Author, string Translator, string Category);
        Task<UploadFileResult> UploadFileAsync(IFormFile file, string path);
        string CheckFileName(string fileName);
    }
}
