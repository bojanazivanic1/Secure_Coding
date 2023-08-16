using System.Linq.Expressions;

namespace InsecureCode.Interfaces.IRepository
{
    public interface IGenericRepository<T> where T : class 
    {
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<T> Get(Expression<Func<T, bool>> expression, List<string>? includes = null);
        Task<IList<T>> GetAll(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null);
        Task Save();
    }
}
