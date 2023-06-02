using CoreRepository.IRepositries;
using DataRepository.Context;
using System.Linq.Expressions;

namespace DataRepository.Repositries
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DataContext _context;
        public BaseRepository(DataContext context)
        {
            _context = context;
        }
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return entity;
        }
        public IEnumerable<T> SaveRange(IEnumerable<T> list)
        {
            _context.AddRange(list);
            return list;
        }
        public int Count(Expression<Func<T, bool>> exp)
        {
            IQueryable<T> query = _context.Set<T>();
            return query.Count(exp);
        }
        public int Count()
        {
            IQueryable<T> query = _context.Set<T>();
            return query.Count();
        }
        public T GetEntity(Expression<Func<T, bool>> exp)
        {
            return _context.Set<T>().FirstOrDefault(exp);
        }
        public IQueryable<T> GetPaged(int page, int pageSize, out int count, Expression<Func<T, bool>> exp)
        {
            IQueryable<T> query = _context.Set<T>().Where(exp).AsQueryable();
            IQueryable<T> entities = query.Skip(((page-1) * pageSize)).Take(pageSize);
            count = query.Count();
            return entities;
        }
        public IQueryable<T> GetPaged(int page, int pageSize, out int count)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();
            IQueryable<T> entities = query.Skip(((page - 1) * pageSize)).Take(pageSize);
            count = query.Count();
            return entities;
        }
        public void DeleteEntity(Expression<Func<T, bool>> exp)
        {
            var entity = _context.Set<T>().FirstOrDefault(exp);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public IQueryable<T> GetOrderdPaged(int page, int pageSize, out int count, Expression<Func<T, object>> exp)
        {
            IQueryable<T> query = _context.Set<T>().OrderBy(exp).AsQueryable();
            IQueryable<T> entities = query.Skip(((page-1) * pageSize)).Take(pageSize);
            count = query.Count();
            return entities;
        }

        public IQueryable<T> GetOrderdDesPaged(int page, int pageSize, out int count, Expression<Func<T, object>> exp)
        {
            IQueryable<T> query = _context.Set<T>().OrderByDescending(exp).AsQueryable();
            IQueryable<T> entities = query.Skip(((page - 1) * pageSize)).Take(pageSize);
            count = query.Count();
            return entities;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
