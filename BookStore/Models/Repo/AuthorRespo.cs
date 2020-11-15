using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repo
{
    public class AuthorRespo : IBookStoreRepository<Author>

    {
        List<Author> Authors;
        public AuthorRespo()
        {
            Authors = new List<Author>()
            {
                new Author { Id=1,Name="khalil1" },
                new Author{Id=2,Name="khalil2"},
                new Author{Id=3,Name="khalil3"},
            };
        }
        public void add(Author entity)
        {
            Authors.Add(entity);
        }

        public void delete(int id)
        {
            var Author = find(id);
            Authors.Remove(Author);
        }

        public Author find(int id)
        {
            var Author = Authors.SingleOrDefault(a => a.Id == id);
            return Author;
        }

        public IList<Author> list()
        {
            return Authors;
        }

        public void update(int id, Author newAuthor)
        {
            var Author = find(id);
            Author.Id = newAuthor.Id;
            Author.Name = newAuthor.Name;
        }
    }
}
