using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using MongoDBUsage.Repository.Entities;

namespace MongoDBUsage.Repository.Repositories.Impl
{
    public class MongoRepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : EntityWithTypedId<TId>
    {
        private readonly MongoCollection<T> _collection;

        public MongoRepositoryWithTypedId()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            var db = server.GetDatabase("Temp");
            var collectionName = InferCollectionNameFrom();
            _collection = db.GetCollection<T>(collectionName);
        }

        private string InferCollectionNameFrom()
        {
            var type = typeof(T);
            return type.Name;
        }

        protected internal MongoCollection<T> Collection
        {
            get { return _collection; }
        }

        public IEnumerable<bool> InsertBatch(IEnumerable<T> entities)
        {
           var result = Collection.InsertBatch(entities);
            return result.Select(n => n.Ok);
        }

        public bool Insert(T entity)
        {
            var result = Collection.Insert(entity);
            return result.Ok;
        }

        public T Get(TId id)
        {
            return Collection.FindOneById(BsonValue.Create(id));
        }

        public bool Save(T entity)
        {
            var result = Collection.Save(entity);
            return result.Ok;
        }

        public bool Delete(TId id)
        {
            var result = Collection.Remove(Query<T>.EQ(t => t.Id, id));
            return result.Ok;
        }

        public IQueryable<T> AsQueryable()
        {
            return Collection.AsQueryable();
        }

        public bool RemoveAll()
        {
            var result = Collection.RemoveAll();
            return result.Ok;
        }
    }

    public class MongoRepository<T> : MongoRepositoryWithTypedId<T, ObjectId>, IRepository<T> where T : Entity
    {

    }
}