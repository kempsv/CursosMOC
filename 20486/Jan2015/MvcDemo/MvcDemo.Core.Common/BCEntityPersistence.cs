using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Core.Common
{
    public abstract class BCEntityPersistence<T> : GenericRepository<T>
    {
        // Instance is setup by Business Component
        protected GenericRepository<T> _persistence = null;
        public override IQueryable<T> Query
        {
            get { return _persistence.Query; }
        }
        public override void Add(T entity)
        {
            _persistence.Add(entity);
            this.Save();
        }
        public override void Delete(T entity)
        {
            _persistence.Delete(entity);
            this.Save();
        }
        public override IEnumerable<T> FindAll()
        {
            return _persistence.FindAll();
        }
        public override void Update(T entity)
        {
            _persistence.Update(entity);
            this.Save();
        }
        public override IEnumerable<T> 
            FindBy(Expression<Func<T, bool>> predicate)
        {
            return _persistence.FindBy(predicate);
        }

        public override void Save()
        {
            _persistence.Save();
        }
    }
}
