using Snipefish.Domain.Entities;

namespace Snipefish.Domain.Repositories.Query
{
    public interface IUserAdventureQueryRepository : IQueryRepository<UserAdventures>
    {
        Task<UserAdventures?> GetUserByEmail(string requestUserEmail, CancellationToken cancellationToken);
    }
}
