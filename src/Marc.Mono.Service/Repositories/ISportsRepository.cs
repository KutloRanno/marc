using System.Linq.Expressions;
using Marc.Mono.Service.Entities;

namespace Marc.Mono.Service.Repositories
{
    public interface ISportsRepository
    {
        Task CreateAsync(Sport sport);
        Task DeleteAsync(int id);
        Task<Sport?> GetAsync(int id);
        Task<IReadOnlyCollection<Sport>> GetAllAsync(Expression<Func<Sport,bool>>filter);
        Task <IReadOnlyCollection<Sport>> GetAllAsync();
        Task UpdateAsync(Sport updatedSport);
        Task<int> CountAsync(string? filter);//thiking about removing this method. I'll count in F-End
    }
}