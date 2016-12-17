using MvcDemo.Core.Common;
using MvcDemo.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Model.Repository
{
    public class RepositoryDb<T> : GenericRepository<T> where T : class
    {
        protected readonly IDbSet<T> _dbset;

        public RepositoryDb(DbContext context)
        {
            _entities = context;
            _dbset = _entities.Set<T>();
        }
        public override IQueryable<T> Query
        {
            get { return _dbset; }
        }
        public override IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = _dbset.AsNoTracking()
                                    .Where(predicate).AsEnumerable();
            return query;
        }

        public override IEnumerable<T> FindAll()
        {
            return _dbset.AsNoTracking().AsEnumerable<T>();
        }

        public override void Delete(T entidade)
        {
            if (_entities.Entry<T>(entidade).State == EntityState.Detached)
                _entities.Entry<T>(entidade).State = EntityState.Deleted;

            _entities.Set<T>().Remove(entidade);
        }
        
        public override void Add(T entidade)
        {
            _entities.Set<T>().Add(entidade);
        }

        public override void Update(T entidade)
        {
            _entities.Entry(entidade).State = EntityState.Modified;
        }

        public override void Save()
        {
            IEnumerable<DbEntityEntry> modifiedEntries = _entities.ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity
                    && (x.State == System.Data.Entity.EntityState.Added
                    || x.State == System.Data.Entity.EntityState.Modified));

            foreach (DbEntityEntry entry in modifiedEntries)
            {
                IAuditableEntity entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    string identityName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                    DateTime now = DateTime.Now;

                    if (entry.State == System.Data.Entity.EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else 
                    {
                        _entities.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        _entities.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }
                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }
            _entities.SaveChanges();
        }
    }
}
