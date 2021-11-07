using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RenewableEnergyCalculator.Models;

namespace RenewableEnergyCalculator.Controllers
{
    public class PanelsController : Controller
    {
        private readonly PanelRepository _dataAccessProvider = new PanelRepository();

        // GET: Panels
        public async Task<ActionResult> Index()
        {
            IEnumerable<Panel> panels = await _dataAccessProvider.GetPanels();
            return View(panels.ToList());
        }

        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panel panel = await _dataAccessProvider.GetPanel(id);

            if (panel == null)
            {
                return HttpNotFound();
            }
            return View(panel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,Manufacturer,Efficency")] Panel panel)
        {
            if (ModelState.IsValid)
            {
                await _dataAccessProvider.Add(panel);
                return RedirectToAction("Index");
            }

            return View(panel);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Panel panel = await _dataAccessProvider.GetPanel(id);
            if (panel == null)
            {
                return HttpNotFound();
            }
            return View(panel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Manufacturer,Efficency")] Panel panel)
        {
            if (ModelState.IsValid)
            {
                await _dataAccessProvider.Update(panel);
                return RedirectToAction("Index");
            }
            return View(panel);
        }

        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Panel panel = await _dataAccessProvider.GetPanel(id);
            if (panel == null)
            {
                return HttpNotFound();
            }
            return View(panel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await _dataAccessProvider.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
