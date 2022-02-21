using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.ViewModels
{
    public class BooksCreateViewModel
    {
        public BooksCreateViewModel( BookSubCategoriesViewModel _SubCategory)
        {
            SubCategories = _SubCategory;
        }

        public BooksCreateViewModel()
        {

        }
        public DateTime? PublishDate { get; set; }
        public IEnumerable<TreeViewCategory> Categories { get; set; }
        public BookSubCategoriesViewModel SubCategories { get; set; }
        public int BookId { get; set; }
        public bool RecentIsPublish { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "عنوان ")]
        public string Title { get; set; }

        [Display(Name = "خلاصه")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "قیمت")]
        public int Price { get; set; }

        [Required(ErrorMessage = "وارد نمودن {0} الزامی است.")]
        [Display(Name = "موجودی")]
        public int Stock { get; set; }
        public string File { get; set; }

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
        public byte[] Image { get; set; }
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

        [Display(Name = "عنوان")]
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
        public DateTime? PublishDate { get; set; }

        [Display(Name = "وضعیت")]
        public bool? IsPublish { get; set; }

        [Display(Name = "نویسندگان")]
        public string Author { get; set; }
        [Display(Name = "مترجمان")]
        public string Tranlator { get; set; }
        [Display(Name = "دسته بندی")]
        public string Category { get; set; }
        [Display(Name = " زبان")]
        public string Languge { get; set; }
    }
    public class BookAdvancedSearchViewModel
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string PublisherName { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Translator { get; set; }
        public string Languge { get; set; }
        public int Stock { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool? IsPublish { get; set; }
        public int Price { get; set; }
    }
    public class ReadAllBook
    {
        public string Title { get; set; }
        public int BookID { get; set; }
        public Int16 Weight { get; set; }
        public byte[] Image { get; set; }
        public string ISBN { get; set; }
        public bool? IsPublish { get; set; }
        public int NumOfPages { get; set; }
        public int Price { get; set; }
        public DateTime? PublishDate { get; set; }
        public int PublishYear { get; set; }
        public int Stock { get; set; }
        public string Summary { get; set; }
        public string LanguageName { get; set; }
        public string PublisherName { get; set; }
        public string Aouthers { get; set; }
        public string Translators { get; set; }
        public string Categories { get; set; }
    }
    public class BookSubCategoriesViewModel
    {
        public BookSubCategoriesViewModel(List<TreeViewCategory> _Categories, int[] _CategoriesId)
        {
            Categories = _Categories;
            CategoryID = _CategoriesId;
        }
        public List<TreeViewCategory> Categories { get; set; }
        public int[] CategoryID { get; set; }
    }
}


