using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.ViewModels
{
    public class BooksCreateEditViewModel
    {
        public BooksCreateEditViewModel(BooksSubCategoriesViewModel _SubCategoriesVM)
        {
            SubCategoriesVM = _SubCategoriesVM;
        }

        public BooksCreateEditViewModel()
        {

        }


        public int BookID { get; set; }

        public DateTime? PublishDate { get; set; }

        public BooksSubCategoriesViewModel SubCategoriesVM { get; set; }

        public IEnumerable<TreeViewCategory> Categories { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "عنوان ")]
        public string Title { get; set; }

        public bool RecentIsPublish { get; set; }

        [Display(Name = "خلاصه")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "موجودی")]
        public int Stock { get; set; }

        public string ImageBase64 { get; set; }

        public byte[] Image { get; set; }

        [Display(Name = "تعداد صفحات")]
        public int NumOfPages { get; set; }

        [Display(Name = "وزن")]
        public short Weight { get; set; }

        [Display(Name = "شابک")]
        public string ISBN { get; set; }

        [Display(Name = " این کتاب روی سایت منتشر شود.")]
        public bool IsPublish { get; set; }


        [Display(Name = "سال انتشار")]
        public int PublishYear { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "زبان")]
        public int LanguageID { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "ناشر")]
        public int PublisherID { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "نویسندگان")]
        public int[] AuthorID { get; set; }

        [Display(Name = "مترجمان")]
        public int[] TranslatorID { get; set; }

        public int[] CategoryID { get; set; }
    }

    public class BooksSubCategoriesViewModel
    {
        public BooksSubCategoriesViewModel(List<TreeViewCategory> _Categories, int[] _CategoryID)
        {
            Categories = _Categories;
            CategoryID = _CategoryID;
        }

        public List<TreeViewCategory> Categories { get; set; }
        public int[] CategoryID { get; set; }
    }

    public class AuthorList
    {
        public int AuthorID { get; set; }
        public string NameFamily { get; set; }
    }

    public class TranslatorList
    {
        public int TranslatorID { get; set; }
        public string NameFamily { get; set; }
    }

    public class BooksIndexViewModel
    {
        public int BookID { get; set; }

        [Display(Name ="عنوان")]
        public string Title { get; set; }

        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Display(Name = "موجودی")]
        public int Stock { get; set; }

        [Display(Name = "شابک")]
        public string ISBN { get; set; }

        [Display(Name = "ناشر")]
        public string PublisherName { get; set; }

        [Display(Name = "تاریخ انتشار در سایت")]
        public string PublishDate { get; set; }

        [Display(Name = "وضعیت")]
        public string IsPublish { get; set; }

        [Display(Name = "نویسندگان")]
        public string Author { get; set; }

        [Display(Name = "مترجمان")]
        public string Translator { get; set; }

        [Display(Name = "دسته بندی ها")]
        public string Category { get; set; }
        public string Language { get; set; }
    }


    public class BooksAdvancedSearch
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Language { get; set; }
        public string Translator { get; set; }
        public string Category { get; set; }
    }

    public class ReadAllBook
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public int NumOfPages { get; set; }
        public short Weight { get; set; }
        public string ISBN { get; set; }
        public bool IsPublish { get; set; }
        public int PublishYear { get; set; }
        public string LanguageName { get; set; }
        public string PublisherName { get; set; }
        public string Authors { get; set; }
        public string Translators { get; set; }
        public string Categories { get; set; }
        public DateTime? PublishDate { get; set; }
        public byte[] Image { get; set; }
    }



}
