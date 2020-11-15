using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repo
{
    public class BookRepository : IBookStoreRepository<Book>
    {
        public void add(Book entity)
        {
            throw new NotImplementedException();
        }

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public Book find(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Book> list()
        {
            throw new NotImplementedException();
        }

        public void update(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
