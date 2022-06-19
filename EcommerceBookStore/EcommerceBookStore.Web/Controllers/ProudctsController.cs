using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcommerceBookStore.Data;
using EcommerceBookStore.Model;

namespace EcommerceBookStore.Web.Controllers
{
    public class ProudctsController : Controller
    {
        private EBookStoreContext db = new EBookStoreContext();

        // GET: Proudcts
        public async Task<ActionResult> Index()
        {
            var proudcts = db.proudcts.Include(p => p.Category).Include(p => p.discount);
            return View(await proudcts.ToListAsync());
        }

        // GET: Proudcts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proudct proudct = await db.proudcts.FindAsync(id);
            if (proudct == null)
            {
                return HttpNotFound();
            }
            return View(proudct);
        }

        // GET: Proudcts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.DiscountId = new SelectList(db.discounts, "Id", "Name");
            return View();
        }

        // POST: Proudcts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Author,PushlingHouse,PubshDate,desc,ProudctInventory,CategoryId,price,DiscountId,Create_at,Modified_at,ProudctImage")] Proudct proudct)
        {
            if (ModelState.IsValid)
            {
                db.proudcts.Add(proudct);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", proudct.CategoryId);
            ViewBag.DiscountId = new SelectList(db.discounts, "Id", "Name", proudct.DiscountId);
            return View(proudct);
        }

        // GET: Proudcts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proudct proudct = await db.proudcts.FindAsync(id);
            if (proudct == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", proudct.CategoryId);
            ViewBag.DiscountId = new SelectList(db.discounts, "Id", "Name", proudct.DiscountId);
            return View(proudct);
        }

        // POST: Proudcts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Author,PushlingHouse,PubshDate,desc,ProudctInventory,CategoryId,price,DiscountId,Create_at,Modified_at,ProudctImage")] Proudct proudct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proudct).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", proudct.CategoryId);
            ViewBag.DiscountId = new SelectList(db.discounts, "Id", "Name", proudct.DiscountId);
            return View(proudct);
        }

        // GET: Proudcts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proudct proudct = await db.proudcts.FindAsync(id);
            if (proudct == null)
            {
                return HttpNotFound();
            }
            return View(proudct);
        }

        // POST: Proudcts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Proudct proudct = await db.proudcts.FindAsync(id);
            db.proudcts.Remove(proudct);
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
