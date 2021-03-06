﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.Repo
{
  public  interface IBookStoreRepository<TEntity>
    {
        IList<TEntity> list();
        TEntity find(int id);
        void add(TEntity entity);

        void update(int id,TEntity entity);

        void delete(int id);
        public List<TEntity> search(string term);

    }
}
