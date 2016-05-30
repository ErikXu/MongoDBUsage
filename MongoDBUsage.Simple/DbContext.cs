using MongoDB.Driver;

namespace MongoDBUsage.Simple
{
    public class DbContext
    {
        private readonly MongoDatabase _db;

        public DbContext()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            _db = server.GetDatabase("Temp");
        }

        public MongoCollection<T> Collection<T>() where T : Entity
        {
            var collectionName = InferCollectionNameFrom<T>();
            return _db.GetCollection<T>(collectionName);
        }

        private static string InferCollectionNameFrom<T>()
        {
            var type = typeof(T);
            return type.Name;
        } 
    }
}