using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repo
{
    public class BookDBrepository : IBookStoreRepository<Book>
    {
        BookStoreDbContext Db;
        public BookDBrepository(BookStoreDbContext db)
        {
            Db = db;
        }
        public void add(Book entity)
        {
          
            Db.Books.Add(entity);
            Db.SaveChanges();
        }

        public void delete(int id)
        {
            var book = find(id);
            Db.Books.Remove(book);
            Db.SaveChanges();
        }

        public Book find(int id)
        {
            var book = Db.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> list()
        {
            return Db.Books.Include(a=>a.Author).ToList();
        }

        public void update(int id, Book newbook)
        {
            Db.Update(newbook);
            Db.SaveChanges();
        }
    }
}
