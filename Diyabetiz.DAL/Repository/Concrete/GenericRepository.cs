using Diyabetiz.DAL.DbContexts;
using Diyabetiz.DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Diyabetiz.DAL.Repository.Concrete
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        private DiyabetizDbContext _context;
        private DbSet<TEntity> _dbSet;
        public GenericRepository(DiyabetizDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
       
        public void Delete(TKey key)
        {
            TEntity entityToDelete = _dbSet.Find(key);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression).FirstOrDefault();
        }

        public TEntity FindById(TKey key)
        {
            return _dbSet.Find(key);
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression != null)
            {
                return _dbSet.Where(expression);
            }
            return _dbSet;
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public void Detach(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
