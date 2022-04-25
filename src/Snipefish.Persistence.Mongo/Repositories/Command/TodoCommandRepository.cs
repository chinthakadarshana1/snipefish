using Snipefish.Domain.Entities;
using Snipefish.Domain.Repositories.Command;
using Snipefish.Persistence.Mongo.DbContext;

namespace Snipefish.Persistence.Mongo.Repositories.Command
{
    public class TodoCommandRepository : MongoCommandRepository<Todo>, ITodoCommandRepository
    {
        public TodoCommandRepository(ISnipefishDbContext dbContext) : base(dbContext, dbContext.TodoCollection)
        {
            CreateIndexIfNotExists(x => x.Name);
        }
    }
}
