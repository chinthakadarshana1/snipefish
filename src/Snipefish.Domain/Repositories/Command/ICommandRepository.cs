using Snipefish.Domain.Entities;

namespace Snipefish.Domain.Repositories.Command
{
    public interface ICommandRepository<in T> where T : EntityBase
    {
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);
    }
}
