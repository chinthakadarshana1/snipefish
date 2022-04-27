using Snipefish.Domain.Entities;

namespace Snipefish.Domain.Repositories.Query
{
    public interface IUserAdventureQueryRepository : IQueryRepository<UserAdventures>
    {
        Task<UserAdventures?> GetUserByUserName(string requestUserName, CancellationToken cancellationToken);
        Task<UserAdventures?> GetUserByUserId(string userId, CancellationToken cancellationToken);
    }
}
