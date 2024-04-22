using System.Linq.Expressions;
using Marc.Mono.Service.Entities;

namespace Marc.Mono.Service.Repositories;

public interface IRepository<T> where T: IEntity
{
    Task CreateAsync(T entity);
        Task DeleteAsync(Guid id);
        Task<T> GetAsync(Guid id);
        Task<IReadOnlyCollection<T>> GetAllAsync(Expression<Func<T,bool>>filter);
        Task <IReadOnlyCollection<T>> GetAllAsync();
        Task UpdateAsync(T updatedItem);
        Task<int> CountAsync(string? filter);//thiking about removing this method. I'll count in F-End
}