using MongoDB.Bson;

namespace MongoDBUsage.Repository.Entities
{
    public abstract class EntityWithTypedId<TId>
    {
        public TId Id { get; set; }
    }

    public abstract class Entity : EntityWithTypedId<ObjectId>
    {
    }
}