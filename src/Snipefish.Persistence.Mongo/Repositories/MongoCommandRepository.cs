using MongoDB.Bson;
using MongoDB.Driver;
using Snipefish.Domain.Entities;
using Snipefish.Domain.Repositories.Command;
using Snipefish.Persistence.Mongo.DbContext;
using System.Linq.Expressions;

namespace Snipefish.Persistence.Mongo.Repositories
{
    public abstract class MongoCommandRepository<TEntity> : ICommandRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly IMongoContext MongoDbContext;
        protected readonly string CollectionName;

        protected IMongoCollection<TEntity> MongoCollection;

        protected MongoCommandRepository(IMongoContext dbContext, string collectionName)
        {
            MongoDbContext = dbContext;
            CollectionName = collectionName;
            MongoCollection = MongoDbContext.GetCollection<TEntity>(collectionName);
        }


        protected void CreateIndexIfNotExists(Expression<Func<TEntity, object>> field, bool isAscending = true)
        {
            var indexBuilder = Builders<TEntity>.IndexKeys;

            var indexModel = isAscending
                ? new CreateIndexModel<TEntity>(indexBuilder.Ascending(field))
                : new CreateIndexModel<TEntity>(indexBuilder.Descending(field));

            MongoCollection.Indexes.CreateOne(indexModel);
        }

        protected void CreateIndexIfNotExists(CreateIndexModel<BsonDocument> index)
        {
            var collectionBson = MongoDbContext.GetCollection<BsonDocument>(CollectionName);
            collectionBson.Indexes.CreateOneAsync(index);
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await MongoCollection.InsertOneAsync(entity, null, cancellationToken);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await MongoCollection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id), entity,
                cancellationToken: cancellationToken);
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await MongoCollection.DeleteOneAsync(Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id), cancellationToken);
        }

    }
}
