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
                    string filename = string.Empty;
                    if (model.file != null)
                    {
                        string uploads = Path.Combine(hosting.WebRootPath,"uploads");
                        filename = model.file.FileName;
                        string fullpath = Path.Combine(uploads, filename);
                        model.file.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
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
                string filename = string.Empty;
                if (viewmodel.file != null)
                {
                    string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                    filename = viewmodel.file.FileName;
                    string fullpath = Path.Combine(uploads, filename);
                    // delete old file 
                    string oldfilename = bookRespo.find(viewmodel.BookId).imageurl;
                    string fulloldpath = Path.Combine(uploads,oldfilename);
                    if (fullpath != fulloldpath)
                    {
                        System.IO.File.Delete(fulloldpath);

                        //save new  file 
                        viewmodel.file.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                }
                var author = authorRepos.find(viewmodel.AuthorId);
                Book book = new Book
                {
                  // Id = viewmodel.BookId,
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
    }
}