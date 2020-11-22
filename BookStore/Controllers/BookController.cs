using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Repo;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> bookRespo;

        public IBookStoreRepository<Author> authorRepos { get; }

        public BookController(IBookStoreRepository<Book> BookRepossitory, IBookStoreRepository<Author> AuthorRepos)
        {
            bookRespo = BookRepossitory;
            authorRepos = AuthorRepos;
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
                Authors = authorRepos.list().ToList()
            };
            return View(model);
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModels model)
        {
            try
            {
                // TODO: Add insert logic here
                var author = authorRepos.find(model.AuthorId);
                Book book = new Book
                {
                    Id=model.BookId,
                    Title=model.Title,
                    Description=model.Description,
                    Author=author

                };
                bookRespo.add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
                Authors=authorRepos.list().ToList()
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
                var author = authorRepos.find(viewmodel.AuthorId);
                Book book = new Book
                {
                  // Id = viewmodel.BookId,
                    Title = viewmodel.Title,
                    Description = viewmodel.Description,
                    Author = author

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
            return View();
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}