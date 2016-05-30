using MongoDBUsage.Repository.Entities;

namespace MongoDBUsage.Repository.Repositories
{
    public interface IStockRepository : IRepository<Stock>
    {
        bool UpdateBatch(double minPrice, string name, double price);

        bool DeleteBatch(double minPrice);
    }
}