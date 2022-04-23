namespace Snipefish.Persistence.Mongo.Configurations
{
    public interface IMongoConfiguration
    {
        public string ConnectionString { get; set; }

        public string DataBaseName { get; set; }
    }
}
