﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BookStore.Models;
using BookStore.Models.Repo;

namespace BookStore.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IBookStoreRepository<Author> authorRespo;

        public AuthorController(IBookStoreRepository<Author> AuthorRespo)
        {
            authorRespo = AuthorRespo;
        }
        // GET: Author
        public ActionResult Index()
        {
            var Authors = authorRespo.list();
            
            return View(Authors);
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            var Author = authorRespo.find(id);
            return View(Author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                // TODO: Add insert logic here
                authorRespo.add(author);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {
            var Author = authorRespo.find(id);
            return View(Author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                // TODO: Add update logic here
                authorRespo.update(id, author);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
           var author= authorRespo.find(id);
            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                // TODO: Add delete logic here
                authorRespo.delete(id);


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}