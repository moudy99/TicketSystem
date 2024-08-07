using System.Linq.Expressions;

namespace TicketSystem.Application.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        void Add(T entity);
        Task AddAsync(T entity);


        T Find(Expression<Func<T, bool>> criteria, string[] includes = null);

        IQueryable<T> FindAllByOrder(string[] includes = null, Expression<Func<T, bool>> criteria = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        IQueryable<T> FindAll(string[] includes = null, Expression<Func<T, bool>> criteria = null);
        Task<IQueryable<T>> FindAllAsync(string[] includes = null, Expression<Func<T, bool>> criteria = null);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

        void Delete(int id);
        Task DeleteAsync(int id);

    }
}
