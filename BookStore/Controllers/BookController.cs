using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Repo;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRespo;

        public IBookStoreRepository<Author> authorRepos { get; }
        public IHostingEnvironment hosting { get; }

        public BookController(IBookStoreRepository<Book> BookRepossitory, IBookStoreRepository<Author> AuthorRepos
            , IHostingEnvironment Hosting)
        {
            bookRespo = BookRepossitory;
            authorRepos = AuthorRepos;
            hosting = Hosting;
        }
        // GET: Book
        public ActionResult Index()
        {
            var books = bookRespo.list();
            return View(books);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRespo.find(id);

            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModels
            {
                Authors = fillselectlist()
            };
            return View(model);
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModels model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    string filename = uploadfile(model.file)??string.Empty;
                    
                    if (model.AuthorId == -1)
                    {
                        ViewBag.message = "Please Select Author From List";
                       
                        return View(getallauthors());
                    }
                    var author = authorRepos.find(model.AuthorId);
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author,
                        imageurl=filename

                    };
                    bookRespo.add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("", "..you have to fill all required fields..");
            return View(getallauthors());
        }

        private string uploadfile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
               
                string fullpath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullpath, FileMode.Create));
                return file.FileName;
            }
            return null;
        }
        private string uploadfile(IFormFile file,string imageurl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                
                string newpath = Path.Combine(uploads, file.FileName);
                // delete old file 
              
                string oldpath = Path.Combine(uploads, imageurl);
                if (oldpath != newpath)
                {
                    System.IO.File.Delete(oldpath);

                    //save new  file 
                    file.CopyTo(new FileStream(newpath, FileMode.Create));

                }
                return file.FileName;
            }
            return imageurl;
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRespo.find(id);
            var authorid = book.Author == null ? book.Author.Id = 0 :  book.Author.Id;
            var viewmodel = new BookAuthorViewModels
            {
                BookId=book.Id,
                Title=book.Title,
                Description=book.Description,
                AuthorId= authorid,
                Authors=authorRepos.list().ToList(),
                imageurl=book.imageurl
            };
            return View(viewmodel);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModels viewmodel)
        {
            try
            {
                // TODO: Add update logic here
                string filename = uploadfile(viewmodel.file,viewmodel.imageurl);
               
                var author = authorRepos.find(viewmodel.AuthorId);
                Book book = new Book
                {
                   Id = viewmodel.BookId,
                    Title = viewmodel.Title,
                    Description = viewmodel.Description,
                    Author = author,
                    imageurl=filename

                };
                bookRespo.update(viewmodel.BookId, book);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRespo.find(id);
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                bookRespo.delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<Author> fillselectlist()
        {
            var authors = authorRepos.list().ToList();
            authors.Insert(0, new Author { Id = -1, Name = "--Please Select Item" });
            return authors;
        }
        public BookAuthorViewModels getallauthors()
        {
            var vmodel = new BookAuthorViewModels
            {
                Authors = fillselectlist()
            };
            return vmodel;
        }
        public ActionResult search(string term)
        {
            var result = bookRespo.search(term);
            return View ("Index", result);
        }
    }
}