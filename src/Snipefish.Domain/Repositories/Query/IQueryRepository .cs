using Snipefish.Domain.Entities;

namespace Snipefish.Domain.Repositories.Query
{
    public interface IQueryRepository<T> where T : EntityBase
    {
        Task<T?> GetById(string id, CancellationToken cancellationToken);
    }
}
