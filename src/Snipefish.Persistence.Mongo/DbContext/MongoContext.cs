using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Snipefish.Domain.Entities;
using Snipefish.Persistence.Mongo.Configurations;

namespace Snipefish.Persistence.Mongo.DbContext
{
    public abstract class MongoContext : IMongoContext
    {
        protected IMongoDatabase MongoDb;

        protected MongoContext(IMongoConfiguration mongoSetting)
        {
            var mongoClient = new MongoClient(mongoSetting.ConnectionString);
            MongoDb = mongoClient.GetDatabase(mongoSetting.DataBaseName);
            SetDocumentIdMapping();
        }

        private void SetDocumentIdMapping()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(EntityBase)))
            {
                BsonClassMap.RegisterClassMap<EntityBase>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                    cm.MapIdProperty(c => c.Id)
                        .SetIdGenerator(StringObjectIdGenerator.Instance)
                        .SetSerializer(new StringSerializer(BsonType.ObjectId));
                });
            }
            SetDocumentMapping();
        }

        protected abstract void SetDocumentMapping();


        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return MongoDb.GetCollection<T>(name);
        }


        public async Task RecreateCollection(string collectionName, CancellationToken cancellationToken)
        {
            await MongoDb.DropCollectionAsync(collectionName, cancellationToken);
            await MongoDb.CreateCollectionAsync(collectionName, null, cancellationToken);
        }

        public async Task DropCollectionAsync(string name, CancellationToken cancellationToken)
        {
            await MongoDb.DropCollectionAsync(name, cancellationToken);
        }
    }
}
