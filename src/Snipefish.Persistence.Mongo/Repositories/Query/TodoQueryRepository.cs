using Snipefish.Domain.Entities;
using Snipefish.Domain.Repositories.Query;
using Snipefish.Persistence.Mongo.DbContext;

namespace Snipefish.Persistence.Mongo.Repositories.Query
{
    public class TodoQueryRepository : MongoQueryRepository<Todo>, ITodoQueryRepository
    {
        public TodoQueryRepository(ISnipefishDbContext dbContext) : base(dbContext, dbContext.TodoCollection)
        {
        }
    }
}
