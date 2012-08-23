using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using InsanelySimpleBlog.System.Policies;

namespace InsanelySimpleBlog.System.Repositories.Implementation
{
    internal sealed class EntityFrameworkRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly IRetryPolicy _retryPolicy;


        public EntityFrameworkRepository(DbContext context, IRetryPolicy retryPolicy)
        {
            if (context == null) throw new ArgumentNullException("context");
            if (retryPolicy == null) throw new ArgumentNullException("retryPolicy");
            _context = context;
            _retryPolicy = retryPolicy;
        }

        public IQueryable<T> All
        {
            get { return _context.Set<T>(); }
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            return includeProperties.Aggregate(All, (current, includeProperty) => current.Include(includeProperty));
        }

        public T Find(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void InsertOrUpdate(T entity, Func<T, int> idFunc)
        {
            if (idFunc(entity) == default(int))
            {
                // New entity
                _context.Set<T>().Add(entity);
            }
            else
            {
                // Existing entity
                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Insert(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _retryPolicy.Execute(() => _context.SaveChanges());
        }

        public void Delete(int id)
        {
            T entity = Find(id);
            _context.Set<T>().Remove(entity);
        }
    }
}