using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Core.Common
{
    public static class GenericRepositoryExtension
    {
        public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
            where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }
    }

    public abstract class GenericRepository<T> : IDisposable
    {
        private bool _disposed = false;
        protected DbContext _entities = null;

        public abstract IQueryable<T> Query { get; }
        public abstract IEnumerable<T> FindAll();
        public abstract void Delete(T entity);
        public abstract void Add(T entity);
        public abstract void Update(T entity);
        public abstract void Save();
        public abstract IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing && null != _entities)
                {
                    _entities.Dispose();
                    _entities = null;
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
