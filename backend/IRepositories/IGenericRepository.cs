using System.Linq.Expressions;

namespace backend.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);

        Task<IReadOnlyList<T>> GetAll();

        Task<bool> Exists(int id);

        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task Delete(T entity);

        IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> filter,
                                        out int totalRowSelected,
                                        out int totalRow,
                                        out int totalPage,
                                        int index = 0, 
                                        int size = 50, 
                                        string[] includes = null);

    }
}
