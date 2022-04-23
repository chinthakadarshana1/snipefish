namespace Snipefish.Persistence.Mongo.DbContext
{
    public interface ISnipefishDbContext : IMongoContext
    {
        string TodoCollection { get; }
    }
}
