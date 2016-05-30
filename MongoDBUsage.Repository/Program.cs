using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDBUsage.Repository.Entities;
using MongoDBUsage.Repository.Repositories.Impl;

namespace MongoDBUsage.Repository
{
    class Program
    {
        static void Main()
        {
            SetConvention();

            var repository = new StockRepository();

            var stocks = new List<Stock>
            {
                new Stock
                {
                    Symbol = "000001", 
                    Name = "股票1", 
                    Price = 100, 
                    Followers = new List<Follower>
                    {
                        new Follower{ Name = "张三", Age = 20 }, 
                        new Follower{ Name = "李四", Age = 22 }, 
                        new Follower{ Name = "王五", Age = 23 }
                    }
                },
                new Stock
                {
                    Symbol = "000002", 
                    Name = "股票2",
                    Price = 200,
                    Followers = new List<Follower>
                    {
                        new Follower{ Name = "张三", Age = 20 }, 
                        new Follower{ Name = "李四", Age = 22 }
                    }
                },
                new Stock
                {
                    Symbol = "000003", 
                    Name = "股票3", 
                    Price = 300,
                    Followers = new List<Follower>
                    {
                        new Follower{ Name = "张三", Age = 20 }
                    }
                },
                new Stock
                {
                    Id = ObjectId.GenerateNewId(), //这里可以自己设定Id，也可以不设，不设的话操作后会自动分配Id
                    Symbol = "000004", 
                    Name = "股票4", 
                    Price = 400
                }
            };

            Console.WriteLine("批量插入");
            var results = repository.InsertBatch(stocks);
            Console.WriteLine(results.Count());
            Console.WriteLine();

            var stock = new Stock
            {
                Id = ObjectId.GenerateNewId(), //这里可以自己设定Id，也可以不设，不设的话操作后会自动分配Id
                Symbol = "000005",
                Name = "股票5",
                Price = 500
            };

            Console.WriteLine("单条插入");
            var result = repository.Insert(stock);
            Console.WriteLine("插入是否成功：{0}", result);
            Console.WriteLine();

            Console.WriteLine("通过Id检索");
            var findedStock = repository.Get(stock.Id);
            Console.WriteLine("Symbol:{0}, Name:{1}, Price:{2}", findedStock.Symbol, findedStock.Name, findedStock.Price);
            Console.WriteLine();

            Console.WriteLine("保存操作，库里有数据");
            stock.Symbol = "000006";
            result = repository.Save(stock);
            Console.WriteLine("保存是否成功：{0}", result);
            Console.WriteLine();

            Console.WriteLine("删除");
            result = repository.Delete(stock.Id);
            Console.WriteLine("删除是否成功：{0}", result);
            Console.WriteLine();

            Console.WriteLine("保存操作，库里没数据");
            result = repository.Save(stock);
            Console.WriteLine("保存是否成功：{0}", result);
            Console.WriteLine();

            Console.WriteLine("简单查询");
            var list = repository.AsQueryable().Where(n => n.Price >= 300).ToList();
            Console.WriteLine("查询结果条数：{0}", list.Count);
            Console.WriteLine();

            Console.WriteLine("复杂类型查询");
            list = repository.AsQueryable().Where(n => n.Followers.Any(f => f.Name == "王五")).ToList();
            Console.WriteLine("查询结果条数：{0}", list.Count);
            Console.WriteLine();

            Console.WriteLine("批量更新");
            result = repository.UpdateBatch(300, "股票300", 299);
            Console.WriteLine("批量更新是否成功：{0}", result);
            Console.WriteLine();

            Console.WriteLine("批量删除");
            result = repository.DeleteBatch(299);
            Console.WriteLine("批量删除更新是否成功：{0}", result);
            Console.WriteLine();

            Console.WriteLine("删除所有记录");
            result = repository.RemoveAll();
            Console.WriteLine("删除所有记录是否成功：{0}", result);
            Console.WriteLine();

            Console.ReadKey();
        }

        private static void SetConvention()
        {
            var pack = new ConventionPack { new IgnoreExtraElementsConvention(true), new IgnoreIfNullConvention(true) };
            ConventionRegistry.Register("IgnoreExtraElements&IgnoreIfNull", pack, type => true);
        }
    }
}
