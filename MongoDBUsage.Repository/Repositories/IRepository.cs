using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDBUsage.Repository.Entities;

namespace MongoDBUsage.Repository.Repositories
{
    public interface IRepositoryWithTypedId<T, in TId> where T : EntityWithTypedId<TId>
    {
        IEnumerable<bool> InsertBatch(IEnumerable<T> entities);
        bool Insert(T entity);
        T Get(TId id);
        bool Save(T entity);
        bool Delete(TId id);
        IQueryable<T> AsQueryable();
        bool RemoveAll();
    }

    public interface IRepository<T> : IRepositoryWithTypedId<T, ObjectId> where T : Entity
    {

    }
}