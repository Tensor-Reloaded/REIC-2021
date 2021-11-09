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
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://reic:reic2021@reic-cluster.mbfck.mongodb.net/REIC-DB?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            _mongoDb = client.GetDatabase("REIC-DB");
        }
        public IMongoCollection<Panel> PanelsCollection
        {
            get
            {
                var panels = _mongoDb.GetCollection<Panel>("Panels");
                return panels;
            }
        }
    }
}