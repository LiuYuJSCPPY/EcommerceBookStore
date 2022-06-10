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
    public class BookStoreUsersController : Controller
    {
        private EBookStoreContext db = new EBookStoreContext();

        // GET: Dashboard/BookStoreUsers
        public async Task<ActionResult> Index()
        {
            return View(await db.BookStoreUsers.ToListAsync());
        }

        // GET: Dashboard/BookStoreUsers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookStoreUser bookStoreUser = await db.BookStoreUsers.FindAsync(id);
            if (bookStoreUser == null)
            {
                return HttpNotFound();
            }
            return View(bookStoreUser);
        }

        // GET: Dashboard/BookStoreUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/BookStoreUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Address,telephone,UserImage,Create_at,Modified_at,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] BookStoreUser bookStoreUser)
        {
            if (ModelState.IsValid)
            {
                db.BookStoreUsers.Add(bookStoreUser);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bookStoreUser);
        }

        // GET: Dashboard/BookStoreUsers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookStoreUser bookStoreUser = await db.BookStoreUsers.FindAsync(id);
            if (bookStoreUser == null)
            {
                return HttpNotFound();
            }
            return View(bookStoreUser);
        }

        // POST: Dashboard/BookStoreUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,Address,telephone,UserImage,Create_at,Modified_at,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] BookStoreUser bookStoreUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookStoreUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bookStoreUser);
        }

        // GET: Dashboard/BookStoreUsers/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookStoreUser bookStoreUser = await db.BookStoreUsers.FindAsync(id);
            if (bookStoreUser == null)
            {
                return HttpNotFound();
            }
            return View(bookStoreUser);
        }

        // POST: Dashboard/BookStoreUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            BookStoreUser bookStoreUser = await db.BookStoreUsers.FindAsync(id);
            db.BookStoreUsers.Remove(bookStoreUser);
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
