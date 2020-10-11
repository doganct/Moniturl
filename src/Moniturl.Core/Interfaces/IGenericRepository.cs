using Moniturl.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moniturl.Core
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetBySpecAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllBySpecAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<T> AddAsync(T model);
        Task<T> UpdateAsync(T model);
        Task Delete(int id);
    }
}
