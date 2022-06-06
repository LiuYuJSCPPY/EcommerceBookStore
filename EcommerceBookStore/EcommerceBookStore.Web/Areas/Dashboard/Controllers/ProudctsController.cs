using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EcommerceBookStore.Data;
using EcommerceBookStore.Model;

namespace EcommerceBookStore.Web.Areas.Dashboard.Controllers
{
    public class ProudctsController : Controller
    {
        private EBookStoreContext db = new EBookStoreContext();

        // GET: Dashboard/Proudcts
        public ActionResult Index()
        {
            var proudcts = db.proudcts.Include(p => p.Category).Include(p => p.discount);
            return View(proudcts.ToList());
        }

        // GET: Dashboard/Proudcts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proudct proudct = db.proudcts.Find(id);
            if (proudct == null)
            {
                return HttpNotFound();
            }
            return View(proudct);
        }

        // GET: Dashboard/Proudcts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.DiscountId = new SelectList(db.discounts, "Id", "Name");
            return View();
        }

        // POST: Dashboard/Proudcts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Author,PushlingHouse,PubshDate,desc,ProudctInventory,CategoryId,price,DiscountId,Create_at,Modified_at,ProudctImage")] Proudct proudct)
        {
            if (ModelState.IsValid)
            {
                db.proudcts.Add(proudct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", proudct.CategoryId);
            ViewBag.DiscountId = new SelectList(db.discounts, "Id", "Name", proudct.DiscountId);
            return View(proudct);
        }

        // GET: Dashboard/Proudcts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proudct proudct = db.proudcts.Find(id);
            if (proudct == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", proudct.CategoryId);
            ViewBag.DiscountId = new SelectList(db.discounts, "Id", "Name", proudct.DiscountId);
            return View(proudct);
        }

        // POST: Dashboard/Proudcts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Author,PushlingHouse,PubshDate,desc,ProudctInventory,CategoryId,price,DiscountId,Create_at,Modified_at,ProudctImage")] Proudct proudct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(proudct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", proudct.CategoryId);
            ViewBag.DiscountId = new SelectList(db.discounts, "Id", "Name", proudct.DiscountId);
            return View(proudct);
        }

        // GET: Dashboard/Proudcts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proudct proudct = db.proudcts.Find(id);
            if (proudct == null)
            {
                return HttpNotFound();
            }
            return View(proudct);
        }

        // POST: Dashboard/Proudcts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Proudct proudct = db.proudcts.Find(id);
            db.proudcts.Remove(proudct);
            db.SaveChanges();
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
