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
using EcommerceBookStore.Service;
using EcommerceBookStore.Web.Areas.Dashboard.ViewModel;
using System.IO;



namespace EcommerceBookStore.Web.Areas.Dashboard.Controllers
{
    public class CategoriesController : Controller
    {
        private EBookStoreContext db = new EBookStoreContext();
        CategoriesService categoriesService = new CategoriesService();
        // GET: Dashboard/Categories
        public ActionResult Index()
        {
            CategoriesService categoriesService = new CategoriesService();
            return View(categoriesService.GetAllCategroies());
        }

        // GET: Dashboard/Categories/Details/5

        public ActionResult Action()
        {
            return PartialView("_Action");
        }

        [HttpPost]
        public JsonResult Action([Bind(Include = "Name")]Category category,HttpPostedFileBase UpImage)
        {
            JsonResult json = new JsonResult();
            bool Result = false;

                string SaveFilePath = Server.MapPath("~/Image/Category/");
                if (!Directory.Exists(SaveFilePath))
                {
                    Directory.CreateDirectory(SaveFilePath);
                }
                string FileName = Path.GetFileName(UpImage.FileName);
                string _FileName = DateTime.Now.ToString("yyyymmssfff") + FileName;
                string Extension = Path.GetExtension(UpImage.FileName);
                string SavePath = Path.Combine(SaveFilePath, _FileName);
                string CategroyImage = "~/Image/Category/" + _FileName;

                if(Extension.ToLower() == ".jpg" || Extension.ToLower() == ".jepg" || Extension.ToLower() == ".png")
                {
                    if(UpImage.ContentLength < 100000000)
                    {
                        UpImage.SaveAs(SavePath);
                        category.CategroyImage = CategroyImage;
                        Result = categoriesService.SaveCategory(category);
                    }
                }

            if (Result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "上傳失敗" };

            }

            return json;

        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Dashboard/Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CategroyImage")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Dashboard/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Dashboard/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CategroyImage")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Dashboard/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Dashboard/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
