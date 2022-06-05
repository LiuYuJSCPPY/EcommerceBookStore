using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using EcommerceBookStore.Data;
using EcommerceBookStore.Model;
using EcommerceBookStore.Server;
using EcommerceBookStore.Web.Areas.Dashboard.ViewModel;

namespace EcommerceBookStore.Web.Areas.Dashboard.Controllers
{
    public class DiscountsController : Controller
    {
        private EBookStoreContext db = new EBookStoreContext();
        private DiscountService discountService = new DiscountService();

        // GET: Dashboard/Discounts
        public ActionResult Index()
        {
            DiscountListViewModel mdoel = new DiscountListViewModel();
            mdoel.discounts = discountService.GetAllDiscount();
            return View(mdoel);
        }

        // GET: Dashboard/Discounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", discount);
        }


        public ActionResult Action()
        {
            return PartialView("_Action");
        }


        [HttpPost]
        public JsonResult Action(Discount discount , HttpPostedFileBase DicountImage)
        {
            JsonResult json = new JsonResult();
            bool Result = false;

            string FilePath = Server.MapPath("~/Image/Discount/");
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            string FileName = Path.GetFileName(DicountImage.FileName);
            string _FileName = DateTime.Now.ToString("yyyymmssfff") + FileName;
            string Extesion = Path.GetExtension(DicountImage.FileName);
            string SavePath = Path.Combine(FilePath, _FileName);
            string ImagePath = "~/Image/Discount/" + _FileName;


            if (Extesion.ToLower() == ".jpg" || Extesion.ToLower() == ".jepg" || Extesion.ToLower() == ".png")
            {
                DicountImage.SaveAs(SavePath);
                discount.DiscountImage = ImagePath;
                discount.Create_at = DateTime.Now;
                discount.Modified_at = DateTime.Now;
                Result = discountService.SaveDiscount(discount);
            }


            Result = discountService.SaveDiscount(discount);

            if (Result)
            {
                json.Data =new { Success = true };
            }
            else
            {
                json.Data = new { Success = false ,Message = "上傳失敗" };
            }

            return json;
        }

        // GET: Dashboard/Discounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Discounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Desc,Discount_Preceint,IsActival")] Discount discount ,HttpPostedFileBase DicountImage)
        {
            if (ModelState.IsValid)
            {




                db.discounts.Add(discount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discount);
        }

        // GET: Dashboard/Discounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // POST: Dashboard/Discounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Desc,DiscountImage,Discount_Preceint,IsActival,Create_at,Modified_at")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discount);
        }

        // GET: Dashboard/Discounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        // POST: Dashboard/Discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Discount discount = db.discounts.Find(id);
            db.discounts.Remove(discount);
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
