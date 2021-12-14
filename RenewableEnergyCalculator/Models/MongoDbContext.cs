using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Extensibility;

namespace RenewableEnergyCalculator.Models
{
    [Log(AttributeTargetMemberAttributes = MulticastAttributes.Public)]
    public class MongoDbContext
    {
        private readonly IMongoDatabase _mongoDb;
        public MongoDbContext()
        {
            var username = System.Configuration.ConfigurationManager.AppSettings["MongoUsername"];
            var password = System.Configuration.ConfigurationManager.AppSettings["MongoPassword"];
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://"+username+":"+password+"@reic-cluster.pexjk.mongodb.net/REIC?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            _mongoDb = client.GetDatabase("REIC-DB");
        }
        public IMongoCollection<SolarPanel> PanelsCollection
        {
            get
            {
                IMongoCollection<SolarPanel> panels = _mongoDb.GetCollection<SolarPanel>("Panels");
                return panels;
            }
        }

        public IMongoCollection<PanelType> PanelTypesCollection
        {
            get {
                IMongoCollection<PanelType> panelTypes = _mongoDb.GetCollection<PanelType>("PanelTypes");
                return panelTypes;
            }
        }
    }
}