using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Diyabetiz.DAL.Repository.Abstract
{
    public interface IGenericRepository<TEntity, TKey>
    {
        TEntity FindById(TKey key);
        IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression = null);
        TEntity Find(Expression<Func<TEntity, bool>> expression);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TKey key);
    }
}
