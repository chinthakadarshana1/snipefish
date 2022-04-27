namespace Snipefish.Persistence.Mongo.Configurations
{
    public class SnipefishDbConfiguration : IMongoConfiguration
    {
        public string ConnectionString { get; set; } = null!;
        public string DataBaseName { get; set; } = null!;
        public string TodoCollection { get; set; } = null!;
        public string UserAdventuresCollection { get; set; } = null!;
    }
}
