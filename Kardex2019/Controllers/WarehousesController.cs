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

namespace Kardex2019.Controllers
{
    public class WarehousesController : Controller
    {
        private KardexEntities3 db = new KardexEntities3();

        // GET: Warehouses
        public async Task<ActionResult> Index()
        {
            return View(await db.Warehouses.ToListAsync());
        }

        // GET: Warehouses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouses warehouses = await db.Warehouses.FindAsync(id);
            if (warehouses == null)
            {
                return HttpNotFound();
            }
            return View(warehouses);
        }

        // GET: Warehouses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "WarehouseId,Name,Address")] Warehouses warehouses)
        {
            if (ModelState.IsValid)
            {
                db.Warehouses.Add(warehouses);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(warehouses);
        }

        // GET: Warehouses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouses warehouses = await db.Warehouses.FindAsync(id);
            if (warehouses == null)
            {
                return HttpNotFound();
            }
            return View(warehouses);
        }

        // POST: Warehouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "WarehouseId,Name,Address")] Warehouses warehouses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(warehouses).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(warehouses);
        }

        // GET: Warehouses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouses warehouses = await db.Warehouses.FindAsync(id);
            if (warehouses == null)
            {
                return HttpNotFound();
            }
            return View(warehouses);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Warehouses warehouses = await db.Warehouses.FindAsync(id);
            db.Warehouses.Remove(warehouses);
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
