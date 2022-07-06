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

namespace EcommerceBookStore.Web.Areas.Dashboard.Controllers
{
    public class OrderItemsController : Controller
    {
        private EBookStoreContext db = new EBookStoreContext();

        // GET: Dashboard/OrderItems
        public async Task<ActionResult> Index()
        {
            var orderItems = db.orderItems.Include(o => o.order).Include(o => o.proudct);
            return View(await orderItems.ToListAsync());
        }

        // GET: Dashboard/OrderItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = await db.orderItems.FindAsync(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // GET: Dashboard/OrderItems/Create
        public ActionResult Create()
        {
            ViewBag.OrderId = new SelectList(db.orders, "Id", "BookStoreUserId");
            ViewBag.ProudctId = new SelectList(db.proudcts, "Id", "Name");
            return View();
        }

        // POST: Dashboard/OrderItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,OrderId,ProudctId,quantity")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                db.orderItems.Add(orderItem);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.OrderId = new SelectList(db.orders, "Id", "BookStoreUserId", orderItem.OrderId);
            ViewBag.ProudctId = new SelectList(db.proudcts, "Id", "Name", orderItem.ProudctId);
            return View(orderItem);
        }

        // GET: Dashboard/OrderItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = await db.orderItems.FindAsync(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderId = new SelectList(db.orders, "Id", "BookStoreUserId", orderItem.OrderId);
            ViewBag.ProudctId = new SelectList(db.proudcts, "Id", "Name", orderItem.ProudctId);
            return View(orderItem);
        }

        // POST: Dashboard/OrderItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,OrderId,ProudctId,quantity")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderItem).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.orders, "Id", "BookStoreUserId", orderItem.OrderId);
            ViewBag.ProudctId = new SelectList(db.proudcts, "Id", "Name", orderItem.ProudctId);
            return View(orderItem);
        }

        // GET: Dashboard/OrderItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = await db.orderItems.FindAsync(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // POST: Dashboard/OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OrderItem orderItem = await db.orderItems.FindAsync(id);
            db.orderItems.Remove(orderItem);
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
