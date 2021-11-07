using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RenewableEnergyCalculator.Models
{
    public class PanelRepository
    {
        MongoDbContext db = new MongoDbContext();

        public async Task Add(Panel panel)
        {
            try
            {
                await db.Panel.InsertOneAsync(panel);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Panel> GetPanel(string id)
        {
            try
            {
                FilterDefinition<Panel> filter = Builders<Panel>.Filter.Eq("Id", id);
                return await db.Panel.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Panel>> GetPanels()
        {
            try
            {
                return await db.Panel.Find(_ => true).ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task Update(Panel panel)
        {
            try
            {
                await db.Panel.ReplaceOneAsync(filter: g => g.Id == panel.Id, replacement: panel);
            }
            catch
            {
                throw;
            }
        }
        public async Task Delete(string id)
        {
            try
            {
                FilterDefinition<Panel> data = Builders<Panel>.Filter.Eq("Id", id);
                await db.Panel.DeleteOneAsync(data);
            }
            catch
            {
                throw;
            }
        }
    }
}