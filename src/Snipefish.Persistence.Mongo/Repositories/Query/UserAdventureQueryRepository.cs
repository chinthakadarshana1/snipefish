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
    }
}
