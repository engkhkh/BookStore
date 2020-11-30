using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repo
{
    public class AuthorDBRepository : IBookStoreRepository<Author>

    {
        BookStoreDbContext Db;
        public AuthorDBRepository(BookStoreDbContext db)
        {
            Db = db;
        }
        public void add(Author entity)
        {
            Db.Authors.Add(entity);
            Db.SaveChanges();
        }

        public void delete(int id)
        {
            var Author = find(id);
            Db.Authors.Remove(Author);
            Db.SaveChanges();
        }

        public Author find(int id)
        {
            var Author = Db.Authors.SingleOrDefault(a => a.Id == id);
            return Author;
        }

        public IList<Author> list()
        {
            return Db.Authors.ToList();
        }

        public List<Author> search(string term)
        {
            return Db.Authors.Where(a => a.Name.Contains(term)).ToList();
        }

        public void update(int id, Author newAuthor)
        {
            //var Author = find(id);
            //Author.Id = newAuthor.Id;
            //Author.Name = newAuthor.Name;
            Db.Authors.Update(newAuthor);
            Db.SaveChanges();
        }
    }
}
