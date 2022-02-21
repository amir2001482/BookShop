using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Models;
using BookShop.Models.Repository;
using BookShop.Models.UnitOfWork;
using BookShop.Models.ViewModels;
using BookSope2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace BookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _UW;
        private readonly BooksRepository _repository;
        private readonly BookShopContext _context;
        public BookController(IUnitOfWork UW, BooksRepository repository , BookShopContext context)
        {
            _UW = UW;
            _repository = repository;
            _context = context;
        }
        public IActionResult Index(string MSG, int page = 1, int row = 5, string sortExpression = "Title", string title = "")
        {
            title = String.IsNullOrEmpty(title) ? "" : title;
            if (MSG != null)
            {
                ViewBag.Error = "ثبت اطلاعات با خطا مواجه شده است لطفا دوباره تلاش کنید";
            }
            List<int> Rows = new List<int>
            {
                5,10,15,20,50,100
            };
            ViewBag.RowID = new SelectList(Rows, row);
            ViewBag.NumOfRow = (page - 1) * row + 1;
            ViewBag.Search = title;
            var PagingModel = PagingList.Create(_repository.GetAllBooks(title, "", "", "", "", "", ""), row, page, sortExpression, "Title");
            PagingModel.RouteValue = new RouteValueDictionary
            {
                {"row",row},
                {"title",title }
            };
            ViewBag.Category = _repository.GetAllCategories();
            ViewBag.LanguageID = new SelectList(_UW.BaseRepository<Language>().ReadAll(), "LanguageName", "LanguageName");
            ViewBag.PublisherID = new SelectList(_UW.BaseRepository<Publisher>().ReadAll(), "PublisherName", "PublisherName");
            ViewBag.AuthorID = new SelectList(_UW.BaseRepository<Author>().ReadAll().Select(t => new AuthorList { AuthorID = t.AuthorID, NameFamily = t.FirstName + " " + t.LastName }), "NameFamily", "NameFamily");
            ViewBag.TranslatorID = new SelectList(_UW.BaseRepository<Translator>().ReadAll().Select(t => new TranslatorList { TranslatorID = t.TranslatorID, NameFamily = t.Name + " " + t.Family }), "NameFamily", "NameFamily");

            return View(PagingModel);
        }
        public IActionResult Create()
        {
            ViewBag.LanguageID = new SelectList(_UW.BaseRepository<Language>().ReadAll(), "LanguageID", "LanguageName");
            ViewBag.PublisherID = new SelectList(_UW.BaseRepository<Publisher>().ReadAll(), "PublisherID", "PublisherName");
            ViewBag.AuthorID = new SelectList(_UW.BaseRepository<Author>().ReadAll().Select(t => new AuthorList { AuthorID = t.AuthorID, NameFamily = t.FirstName + " " + t.LastName }), "AuthorID", "NameFamily");
            ViewBag.TranslatorID = new SelectList(_UW.BaseRepository<Translator>().ReadAll().Select(t => new TranslatorList { TranslatorID = t.TranslatorID, NameFamily = t.Name + " " + t.Family }), "TranslatorID", "NameFamily");

            BooksCreateViewModel ViewModel = new BooksCreateViewModel();
            ViewModel.SubCategories = new BookSubCategoriesViewModel(_repository.GetAllCategories(), null);
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BooksCreateViewModel ViewModel)
        {
            if (ModelState.IsValid)
            {

                List<Book_Translator> translators = new List<Book_Translator>();
                List<Book_Category> categories = new List<Book_Category>();
                DateTime? PublishDate = null;
                if (ViewModel.TranslatorID != null)
                    translators = ViewModel.TranslatorID.Select(a => new Book_Translator { TranslatorID = a }).ToList();
                if (ViewModel.CategoryID != null)
                    categories = ViewModel.CategoryID.Select(a => new Book_Category { CategoryID = a }).ToList();
                if (ViewModel.IsPublish == true)
                {
                    PublishDate = DateTime.Now;
                }
                try
                {
                    Book book = new Book()
                    {
                        Delete = false,
                        ISBN = ViewModel.ISBN,
                        IsPublish = ViewModel.IsPublish,
                        NumOfPages = ViewModel.NumOfPages,
                        Stock = ViewModel.Stock,
                        Price = ViewModel.Price,
                        LanguageID = ViewModel.LanguageID,
                        Summary = ViewModel.Summary,
                        Title = ViewModel.Title,
                        PublishYear = ViewModel.PublishYear,
                        PublishDate = PublishDate,
                        Weight = ViewModel.Weight,
                        PublisherID = ViewModel.PublisherID,
                        Author_Books = ViewModel.AuthorID.Select(a => new Author_Book { AuthorID = a }).ToList(),
                        book_Categories = categories,
                        book_Tranlators = translators,
                    };
                    await _UW.BaseRepository<Book>().Create(book);
                    await _UW.Commit();
                    return RedirectToAction("Index");

                }
                catch
                {

                    return RedirectToAction("Index", new { MSG = "Faild" });
                }
            }
            else
            {
                ViewBag.LanguageID = new SelectList(_UW.BaseRepository<Language>().ReadAll(), "LanguageID", "LanguageName");
                ViewBag.PublisherID = new SelectList(_UW.BaseRepository<Publisher>().ReadAll(), "PublisherID", "PublisherName");
                ViewBag.AuthorID = new SelectList(_UW.BaseRepository<Author>().ReadAll().Select(t => new AuthorList { AuthorID = t.AuthorID, NameFamily = t.FirstName + " " + t.LastName }), "AuthorID", "NameFamily");
                ViewBag.TranslatorID = new SelectList(_UW.BaseRepository<Translator>().ReadAll().Select(t => new TranslatorList { TranslatorID = t.TranslatorID, NameFamily = t.Name + " " + t.Family }), "TranslatorID", "NameFamily");
                ViewModel.Categories = _repository.GetAllCategories();
                return View(ViewModel);
            }

        }
        public IActionResult AdvancedSearch(BookAdvancedSearchViewModel viewModel/*, int page = 1, int row = 5*/)
        {
            //List<int> Rows = new List<int>
            //{
            //    5,10,15,20,50,100
            //};

            //ViewBag.RowID = new SelectList(Rows, row);
            //ViewBag.NumOfRow = (page - 1) * row + 1;
            viewModel.Title = string.IsNullOrEmpty(viewModel.Title) ? "" : viewModel.Title;
            viewModel.Author = string.IsNullOrEmpty(viewModel.Author) ? "" : viewModel.Author;
            viewModel.Category = string.IsNullOrEmpty(viewModel.Category) ? "" : viewModel.Category;
            viewModel.ISBN = string.IsNullOrEmpty(viewModel.ISBN) ? "" : viewModel.ISBN;
            viewModel.Languge = string.IsNullOrEmpty(viewModel.Languge) ? "" : viewModel.Languge;
            viewModel.Translator = string.IsNullOrEmpty(viewModel.Translator) ? "" : viewModel.Translator;
            viewModel.PublisherName = string.IsNullOrEmpty(viewModel.PublisherName) ? "" : viewModel.PublisherName;
            var Books = _repository.GetAllBooks(viewModel.Title, viewModel.Category, viewModel.Author, viewModel.Translator, viewModel.PublisherName, viewModel.ISBN, viewModel.Languge);
            //var PagingModel = PagingList.Create(Books, row, page);
            return View(Books);
        }
        public IActionResult Detials(int id)
        {
            // Execute data from DB with SQlRowQuery
            // ***********************************************************************************************************//


            //var Book = _context.Books.FromSql("select * From BookInfo as b Where b.BookID = {0} ", id)
            //    .Include(p => p.Publisher)
            //    .Include(tb => tb.book_Tranlators).ThenInclude(t => t.Translator).First();
            ////ViewBag.Aouthers = _context.Books.FromSql("EXEC dbo.GetAouthers {0}", id).ToList();
            //ViewBag.Aouthers = (from b in _context.Author_Books
            //                    join a in _context.Authors on b.AuthorID equals a.AuthorID
            //                    where b.BookID == id
            //                    select new Author
            //                    {
            //                        AuthorID = a.AuthorID,
            //                        FirstName = a.FirstName,
            //                        LastName = a.LastName
            //                    }).ToList();

            //ViewBag.Publishers = (from b in _context.Books
            //                      join p in _context.Publishers on b.PublisherID equals p.PublisherID
            //                      where b.BookID == id
            //                      select new Publisher
            //                      {
            //                          PublisherID = p.PublisherID,
            //                          PublisherName = p.PublisherName
            //                      }).ToList();
            //ViewBag.Categories = (from cb in _context.Book_Categories
            //                      join c in _context.Categories on cb.CategoryID equals c.CategoryID
            //                      where cb.BookID == id
            //                      select new Category
            //                      {
            //                          CategoryID = c.CategoryID,
            //                          CategoryName = c.CategoryName
            //                      }).ToList();

            //*********************************************************************************************************
            //Execute Data From DB With QueryType 
            var BookInfo = _UW._Context.Query<ReadAllBook>().Where(b => b.BookID == id).First();
            //var Book = _UW.BaseRepository<Book>().ReadbyId(id);

            //var BookInfo = _context.ReadAllBook.FromSql("SELECT b.BookID, b.Title, b.Weight, b.Image, b.ISBN, b.IsPublish, b.NumOfPages, b.Price, b.PublishDate, b.PublishYear, b.Stock, b.Summary, l.LanguageName, p.PublisherName, dbo.GetAllAouthers(b.BookID) AS Aouthers, dbo.GetAllCategories(b.BookID) AS Categories, dbo.GetAllTranslators(b.BookID) AS Translators FROM dbo.BookInfo AS b INNER JOIN dbo.Languages AS l ON b.LanguageID = l.LanguageID INNER JOIN dbo.Publishers AS p ON b.PublisherID = p.PublisherID WHERE(b.[Delete] = 0)")
            //                                            .Where(b => b.BookID == id).First();

            //**************************************************************************************************
            //Executed Data From DB With QueryType and  Sql Row Query


            //var BookInfo = _context.Query<ReadAllBook>().Where(b => b.BookID == id).First();
            return View(BookInfo);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var book = await _UW.BaseRepository<Book>().ReadByIdAsync(Id);
            book.Delete = true;
            await _UW.Commit();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
          
                var Books =  _UW.BaseRepository<Book>().ReadByIdAsync(Id);
                if (Books == null)
                {
                    return NotFound();
                }
                
                
                    var ViewModelEdit = await (from b in _UW._Context.Books
                                         .Include(p => p.Publisher)
                                         .Include(l => l.Language)
                                         .Where(b=>b.BookID==Id)
                                         select new BooksCreateViewModel
                                         {
                                             BookId = b.BookID,
                                             ISBN = b.ISBN,
                                             IsPublish = b.IsPublish ?? false,
                                             LanguageID = b.LanguageID,
                                             NumOfPages = b.NumOfPages,
                                             Price = b.Price,
                                             PublisherID = b.Publisher.PublisherID,
                                             PublishYear = b.PublishYear,
                                             Stock = b.Stock,
                                             Summary = b.Summary,
                                             RecentIsPublish =b.IsPublish ?? false,
                                             Title = b.Title,
                                             Weight = b.Weight,
                                             PublishDate = b.PublishDate

                                         }).FirstAsync();
                    //var Book = _context.Books.FromSql("select * From BookInfo as b Where b.BookID = {0} ", Id)
                    //   .Include(p => p.Publisher)
                    //   .Include(l => l.Language).First();
                    //var ViewModelEdit = new BooksCreateViewModel()
                    //{
                    //    BookId = Book.BookID,
                    //    File = Book.File,
                    //    Image = Book.Image,
                    //    ISBN = Book.ISBN,
                    //    IsPublish = (bool)Book.IsPublish,
                    //    NumOfPages = Book.NumOfPages,
                    //    LanguageID = Book.LanguageID,
                    //    Price = Book.Price,
                    //    PublishDate = Book.PublishDate,
                    //    PublishYear = Book.PublishYear,
                    //    RecentIsPublish = (bool)Book.IsPublish,
                    //    Stock = Book.Stock,
                    //    PublisherID = Book.PublisherID,
                    //    Summary = Book.Summary,
                    //    Title = Book.Title,
                    //    Weight = Book.Weight

                    //};
                    //گرفتن ای دی تمام نویسندگان و مترجمان و دسته بنده ها و ریختن آنها در یک آرایه
                    int[] AouthersArray = await (from a in _UW._Context.Author_Books
                                                 where (a.BookID == Id)
                                                 select a.AuthorID).ToArrayAsync();
                    int[] TranslatorsArray = await (from t in _UW._Context.Book_Translators
                                                    where (t.BookID == Id)
                                                    select t.TranslatorID).ToArrayAsync();
                    int[] CategoriesArray = await (from c in _UW._Context.Book_Categories
                                                   where (c.BookID == Id)
                                                   select c.CategoryID).ToArrayAsync();
                    //مقدار دهی پراپرتی های مربوط به دسته بندی و نویسندگان و مترجمان در ویو مدل



                    ViewModelEdit.AuthorID = AouthersArray;

                    ViewModelEdit.CategoryID = CategoriesArray;

                    ViewModelEdit.TranslatorID = TranslatorsArray;
                    //مقدار دهی ویو بگ ها نمایشی مربوط به سلکت لیست ها و نمایش درختی دسته بندی 
                    ViewBag.LanguageID = new SelectList(_UW.BaseRepository<Language>().ReadAll(), "LanguageID", "LanguageName");
                    ViewBag.PublisherID = new SelectList(_UW.BaseRepository<Publisher>().ReadAll(), "PublisherID", "PublisherName");
                    ViewBag.AuthorID = new SelectList(_UW.BaseRepository<Author>().ReadAll().Select(t => new AuthorList { AuthorID = t.AuthorID, NameFamily = t.FirstName + " " + t.LastName }), "AuthorID", "NameFamily");
                    ViewBag.TranslatorID = new SelectList(_UW.BaseRepository<Translator>().ReadAll().Select(t => new TranslatorList { TranslatorID = t.TranslatorID, NameFamily = t.Name + " " + t.Family }), "TranslatorID", "NameFamily");
                    ViewModelEdit.SubCategories = new BookSubCategoriesViewModel(_repository.GetAllCategories(), CategoriesArray);

                    return View( ViewModelEdit);


                

            

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BooksCreateViewModel viewModel)
        {
            //مقدار ئهی ویو بگ های نمایشی سلکت لیست و نمودار درختی
            ViewBag.LanguageID = new SelectList(_UW.BaseRepository<Language>().ReadAll(), "LanguageID", "LanguageName");
            ViewBag.PublisherID = new SelectList(_UW.BaseRepository<Publisher>().ReadAll(), "PublisherID", "PublisherName");
            ViewBag.AuthorID = new SelectList(_UW.BaseRepository<Author>().ReadAll().Select(t => new AuthorList { AuthorID = t.AuthorID, NameFamily = t.FirstName + " " + t.LastName }), "AuthorID", "NameFamily");
            ViewBag.TranslatorID = new SelectList(_UW.BaseRepository<Translator>().ReadAll().Select(t => new TranslatorList { TranslatorID = t.TranslatorID, NameFamily = t.Name + " " + t.Family }), "TranslatorID", "NameFamily");
            viewModel.SubCategories = new BookSubCategoriesViewModel(_repository.GetAllCategories(), viewModel.CategoryID);

            //اعتبار سنجی فرم سمت سرور
            if (ModelState.IsValid)
            {
                //مقدار دهی پراپرتی تاریخ انتشار بر اساس تغییرات
                DateTime? publishdate;
                if (viewModel.RecentIsPublish == false && viewModel.IsPublish == true)
                {
                    publishdate = DateTime.Now;
                }
                else if (viewModel.RecentIsPublish == true && viewModel.IsPublish == false)
                {
                    publishdate = null;
                }
                else
                {
                    publishdate = viewModel.PublishDate;
                }
                //ساخت نمونه جدید از کلاس کتاب ها جهت آپدیت کردن
                Book book = new Book()
                {
                    BookID = viewModel.BookId,
                    Delete = false,
                    File = viewModel.File,
                    Image = viewModel.Image,
                    ISBN = viewModel.ISBN,
                    LanguageID = viewModel.LanguageID,
                    NumOfPages = viewModel.NumOfPages,
                    Price = viewModel.Price,
                    PublisherID = viewModel.PublisherID,
                    PublishYear = viewModel.PublishYear,
                    Stock = viewModel.Stock,
                    Summary = viewModel.Summary,
                    Title = viewModel.Title,
                    Weight = viewModel.Weight,
                    PublishDate = publishdate,
                };
                 _UW.BaseRepository<Book>().Update(book);
                //گرفتن
                var ResentAoutherId = await (from ab in _UW._Context.Author_Books
                                             where (ab.BookID == viewModel.BookId)
                                             select ab.AuthorID).ToArrayAsync();

                var RecentTranslatorsId = await (from a in _UW._Context.Book_Translators
                                                 where (a.BookID == viewModel.BookId)
                                                 select a.TranslatorID).ToArrayAsync();

                var RecenCategoriesId = await (from a in _UW._Context.Book_Categories
                                               where (a.BookID == viewModel.BookId)
                                               select a.CategoryID).ToArrayAsync();

                var DeleteAoutherdId = ResentAoutherId.Except(viewModel.AuthorID);
                var DeleteTranslatorsId = RecentTranslatorsId.Except(viewModel.TranslatorID);
                var DeleteCategoriesId = RecenCategoriesId.Except(viewModel.CategoryID);


                var AddAouthersId = viewModel.AuthorID.Except(ResentAoutherId);
                var AddTranslatorsId = viewModel.TranslatorID.Except(RecentTranslatorsId);
                var AddCategoriesId = viewModel.CategoryID.Except(RecenCategoriesId);



                if (DeleteAoutherdId.Count() != 0)
                   _UW.BaseRepository<Author_Book>().DeleteRange(DeleteAoutherdId.Select(a => new Author_Book() { AuthorID = a, BookID = viewModel.BookId }).ToList());

                if (DeleteTranslatorsId.Count() != 0)
                    _UW.BaseRepository<Book_Translator>().DeleteRange(DeleteTranslatorsId.Select(a => new Book_Translator() { TranslatorID = a, BookID = viewModel.BookId }).ToList());

                if (DeleteCategoriesId.Count() != 0)
                    _UW.BaseRepository<Book_Category>().DeleteRange(DeleteCategoriesId.Select(a => new Book_Category() { CategoryID = a, BookID = viewModel.BookId }).ToList());



                if (AddAouthersId.Count() != 0)
                   await _UW.BaseRepository<Author_Book>().CreateRange(AddAouthersId.Select(a => new Author_Book() { AuthorID = a, BookID = viewModel.BookId }).ToList());

                if (AddTranslatorsId.Count() != 0)
                    await _UW.BaseRepository<Book_Translator>().CreateRange(AddTranslatorsId.Select(a => new Book_Translator() { TranslatorID = a, BookID = viewModel.BookId }).ToList());

                if (AddCategoriesId.Count() != 0)
                    await _UW.BaseRepository<Book_Category>().CreateRange(AddCategoriesId.Select(a => new Book_Category() { CategoryID = a, BookID = viewModel.BookId }).ToList());







                await _UW.Commit();
                ViewBag.MSGSucsses = "عملیات با موفقیت انجام شد";
                return View(viewModel);





                //ViewBag.MSGFaid = "در ویرایش اطلاعات خطایی رخ داده است";
                //return View(viewModel);


            }
            else
            {

                ViewBag.MSGFaid = "اطلاعات فرم نا معتبر است";
                return View(viewModel);
            }

        }
        public async Task<IActionResult> SearchISBN(string ISBN)
        {
            if (ISBN==null)
            {
                ViewBag.MSG = "مقدار شابک را وارد کنید";
            }
            else
            {
                var BookInfo = await (from b in _UW._Context.Books
                          .Where(b => b.ISBN == ISBN)
                                      select new BooksIndexViewModel
                                      {
                                          Author = BookShopContext.GetAllAouther(b.BookID),
                                          Category = BookShopContext.GetAllCategories(b.BookID),
                                          Title = b.Title,
                                          Tranlator = BookShopContext.GetAllTranslators(b.BookID),
                                      }).FirstOrDefaultAsync();
                if (BookInfo == null)
                {
                    ViewBag.Msg = "چنین کتابی وجود ندارد";
                }
                return View(BookInfo);

            }
            return View();



            
        }


    }
}

