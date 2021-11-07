using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RenewableEnergyCalculator.Models
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _mongoDb;
        public MongoDbContext()
        {
            var client = new MongoClient("mongodb+srv://reic:<password>@reic-cluster.mbfck.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            _mongoDb = client.GetDatabase("REIC-DB");
        }
        public IMongoCollection<Panel> Panel
        {
            get
            {
                return _mongoDb.GetCollection<Panel>("Panel");
            }
        }
    }
}