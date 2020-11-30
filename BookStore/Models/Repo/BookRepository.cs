using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repo
{
    public class BookRepossitory : IBookStoreRepository<Book>
    {
        List<Book> books;
        public BookRepossitory()
        {
            books = new List<Book>()
                {
                    new Book
                    {
                        Id=1, Title="c#Book",Description="about c#",imageurl="17-8-2020.jpeg",Author=new Author{Id=2}
                    },
                    new Book
                    {
                        Id=2, Title="JavaBook",Description="about java",imageurl="555.jpg",Author=new Author()
                    },
                    new Book
                    {
                        Id=3, Title="asp.net Book",Description="about asp.net ",imageurl="555.jpg",Author=new Author()
                    },

                };
        }
        public void add(Book entity)
        {
            entity.Id = books.Max(b=>b.Id) + 1;
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

        public List<Book> search(string term)
        {
            return books.Where(a => a.Title.Contains(term)).ToList();
        }

        public void update(int id,Book newbook)
        {
            var book = find(id);
            book.Title = newbook.Title;
            book.Description = newbook.Description;
            book.Author = newbook.Author;
            book.imageurl = newbook.imageurl;
        }
    }
}
