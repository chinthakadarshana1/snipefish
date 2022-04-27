using MongoDB.Driver;

namespace Snipefish.Persistence.Mongo.DbContext
{
    public interface IMongoContext
    {
        public IMongoCollection<T> GetCollection<T>(string name);

        public Task DropCollectionAsync(string name, CancellationToken cancellationToken);

        public Task RecreateCollection(string collectionName, CancellationToken cancellationToken);
    }
}
