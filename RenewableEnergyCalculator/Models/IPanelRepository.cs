using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RenewableEnergyCalculator.Models
{
    public interface IPanelRepository
    {
        Task Add(Panel panel);
        Task Update(Panel panel);
        Task Delete(string id);
        Task<Panel> GetPanel(string id);
        Task<IEnumerable<Panel>> GetPanels();
    }
}