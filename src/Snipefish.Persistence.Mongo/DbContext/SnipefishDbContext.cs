using MongoDB.Bson.Serialization;
using Snipefish.Persistence.Mongo.Configurations;

namespace Snipefish.Persistence.Mongo.DbContext
{
    public class SnipefishDbContext : MongoContext, ISnipefishDbContext
    {
        private readonly SnipefishDbConfiguration _snipefishDbConfiguration;

        public SnipefishDbContext(SnipefishDbConfiguration mongoSetting) : base(mongoSetting)
        {
            _snipefishDbConfiguration = mongoSetting;
        }

        protected override void SetDocumentMapping()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Domain.Entities.Todo)))
            {
                BsonClassMap.RegisterClassMap<Domain.Entities.Todo>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public string TodoCollection => _snipefishDbConfiguration.TodoCollection; 
        public string UserAdventuresCollection => _snipefishDbConfiguration.UserAdventuresCollection;
    }
}
