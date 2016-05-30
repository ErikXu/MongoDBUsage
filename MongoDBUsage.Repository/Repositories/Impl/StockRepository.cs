using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDBUsage.Repository.Entities;

namespace MongoDBUsage.Repository.Repositories.Impl
{
    public class StockRepository : MongoRepository<Stock>, IStockRepository
    {
        public bool UpdateBatch(double minPrice, string name, double price)
        {
            var query = Query<Stock>.Where(n => n.Price >= minPrice);
            var update = Update<Stock>.Set(n => n.Name, name)
                                      .Set(n => n.Price, price);

            var result = Collection.Update(query, update, UpdateFlags.Multi);
            return result.Ok;
        }

        public bool DeleteBatch(double minPrice)
        {
            var result = Collection.Remove(Query<Stock>.Where(n => n.Price >= minPrice));
            return result.Ok;
        }
    }
}