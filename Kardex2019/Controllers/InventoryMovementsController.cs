using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kardex2019.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace Kardex2019.Controllers
{
    public class InventoryMovementsController : Controller
    {
        private KardexEntities3 db = new KardexEntities3();

        public async Task<ActionResult> Report()
        {
            return View(await db.InventoryMovements.ToListAsync());
        }

        public async Task<ActionResult> exportReport()
        {
            ReportDocument reportDocument = new ReportDocument();
            reportDocument.Load(Path.Combine(Server.MapPath("~/Reporte"), "CrystalReport.rpt"));
            reportDocument.SetDataSource(await db.InventoryMovements.ToListAsync());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = reportDocument.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "InventoryReport.pdf");
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: InventoryMovements
        public async Task<ActionResult> Index()
        {
            return View(await db.InventoryMovements.ToListAsync());
        }

        // GET: InventoryMovements/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryMovements inventoryMovements = await db.InventoryMovements.FindAsync(id);
            if (inventoryMovements == null)
            {
                return HttpNotFound();
            }
            return View(inventoryMovements);
        }

        // GET: InventoryMovements/Create
        public ActionResult Create()
        {
            ViewBag.Products = new SelectList(db.Products, "ProductId", "Name");
            ViewBag.Warehouses = new SelectList(db.Warehouses, "WarehouseId", "Name");
            return View();
        }

        // POST: InventoryMovements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "InventoryMovementId,ProductId,WarehouseId,DateTime,Type,Quantity,UnitPrice")] InventoryMovements inventoryMovements)
        {
            if (ModelState.IsValid)
            {
                db.InventoryMovements.Add(inventoryMovements);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(inventoryMovements);
        }

        // GET: InventoryMovements/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryMovements inventoryMovements = await db.InventoryMovements.FindAsync(id);
            if (inventoryMovements == null)
            {
                return HttpNotFound();
            }
            return View(inventoryMovements);
        }

        // POST: InventoryMovements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "InventoryMovementId,ProductId,WarehouseId,DateTime,Type,Quantity,UnitPrice")] InventoryMovements inventoryMovements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventoryMovements).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(inventoryMovements);
        }

        // GET: InventoryMovements/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryMovements inventoryMovements = await db.InventoryMovements.FindAsync(id);
            if (inventoryMovements == null)
            {
                return HttpNotFound();
            }
            return View(inventoryMovements);
        }

        // POST: InventoryMovements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            InventoryMovements inventoryMovements = await db.InventoryMovements.FindAsync(id);
            db.InventoryMovements.Remove(inventoryMovements);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
