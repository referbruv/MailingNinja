using MailingNinja.Contracts.Data.Repositories;
using MailingNinja.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MailingNinja.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public void Delete(object id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }
        }

        public T Get(object id)
        {
            var x = _dbSet.Find(id);
            return x;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}