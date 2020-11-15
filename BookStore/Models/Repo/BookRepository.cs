using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repo
{
    public class BookRepository : IBookStoreRepository<Book>
    {
        List<Book> books;
        public BookRepository()
        {
            books = new List<Book>()
                {
                    new Book
                    {
                        Id=1, Title="c#Book",Description="about c#"
                    },
                    new Book
                    {
                        Id=2, Title="JavaBook",Description="about java"
                    },
                    new Book
                    {
                        Id=3, Title="asp.net Book",Description="about asp.net "
                    },

                };
        }
        public void add(Book entity)
        {
            books.Add(entity);
        }

        public void delete(int id)
        {
            var book = find(id);
            books.Remove(book);
        }

        public Book find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> list()
        {
            return books;
        }

        public void update(int id,Book newbook)
        {
            var book = find(id);
            book.Title = newbook.Title;
            book.Title = newbook.Description;
            book.Author = newbook.Author;
        }
    }
}
