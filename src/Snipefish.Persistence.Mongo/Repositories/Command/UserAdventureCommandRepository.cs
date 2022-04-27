using Snipefish.Domain.Entities;
using Snipefish.Domain.Repositories.Command;
using Snipefish.Persistence.Mongo.DbContext;

namespace Snipefish.Persistence.Mongo.Repositories.Command
{
    public class UserAdventureCommandRepository : MongoCommandRepository<UserAdventures>, IUserAdventureCommandRepository
    {
        public UserAdventureCommandRepository(ISnipefishDbContext dbContext) : base(dbContext, dbContext.UserAdventuresCollection)
        {
            CreateIndexIfNotExists(x => x.UserId);
        }
    }
}
