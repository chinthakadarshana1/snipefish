using MongoDB.Bson;
using MongoDB.Driver;
using Snipefish.Domain.Entities;
using Snipefish.Domain.Repositories.Query;
using Snipefish.Persistence.Mongo.DbContext;

namespace Snipefish.Persistence.Mongo.Repositories
{
    public abstract class MongoQueryRepository<TEntity> : IQueryRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly IMongoContext MongoContext;
        protected readonly string CollectionName;

        protected IMongoCollection<TEntity> MongoCollection;

        protected MongoQueryRepository(IMongoContext context, string collectionName)
        {
            MongoContext = context;
            CollectionName = collectionName;
            MongoCollection = MongoContext.GetCollection<TEntity>(collectionName);
        }


        public virtual async Task<TEntity?> GetById(string id, CancellationToken cancellationToken)
        {
            var objectId = new ObjectId(id);
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            return await MongoCollection.FindAsync(filter, cancellationToken: cancellationToken).Result
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
