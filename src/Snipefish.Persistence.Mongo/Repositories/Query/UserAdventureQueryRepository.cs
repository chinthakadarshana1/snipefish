using MongoDB.Driver;
using Snipefish.Domain.Entities;
using Snipefish.Domain.Repositories.Query;
using Snipefish.Persistence.Mongo.DbContext;

namespace Snipefish.Persistence.Mongo.Repositories.Query
{
    public class UserAdventureQueryRepository : MongoQueryRepository<UserAdventures>, IUserAdventureQueryRepository
    {
        public UserAdventureQueryRepository(ISnipefishDbContext dbContext) : base(dbContext, dbContext.UserAdventuresCollection)
        {
        }

        public async Task<UserAdventures?> GetUserByEmail(string requestUserEmail, CancellationToken cancellationToken)
        {
            return await MongoCollection.Find(c => c.UserName.ToLower() == requestUserEmail.ToLower() )
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
