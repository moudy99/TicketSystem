using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicketSystem.Application.Interfaces.Repository;

namespace TicketSystem.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _dbSet.Where(criteria);
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query.FirstOrDefault();
        }

        public IQueryable<T> FindAllByOrder(
    string[] includes = null,
    Expression<Func<T, bool>> criteria = null,
    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (criteria != null)
            {
                query = query.Where(criteria);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }


        public IQueryable<T> FindAll(string[] includes = null, Expression<Func<T, bool>> criteria = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (criteria != null)
            {
                query = query.Where(criteria);
            }

            return query;
        }

        public async Task<IQueryable<T>> FindAllAsync(string[] includes = null, Expression<Func<T, bool>> criteria = null)
        {
            IQueryable<T> query = _dbSet;
            if (criteria != null)
            {
                query = query.Where(criteria);
            }
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var result = await query.ToListAsync();
            return result.AsQueryable();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}