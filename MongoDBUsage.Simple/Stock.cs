using System.Collections.Generic;

namespace MongoDBUsage.Simple
{
    public class Stock : Entity
    {
        public string Symbol { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public List<Follower> Followers { get; set; }
    }

    public class Follower
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}