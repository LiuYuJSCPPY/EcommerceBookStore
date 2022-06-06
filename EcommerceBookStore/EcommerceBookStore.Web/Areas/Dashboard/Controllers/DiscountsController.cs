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
        private EBookStoreContext _db = new EBookStoreContext();
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
            Discount discount = _db.discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", discount);
        }


        public ActionResult Action(int? Id)
        {

           DiscountViewModel model = new DiscountViewModel();
         
           if(Id.HasValue)
            { 
                var discount = discountService.GetDiscount(Id.Value);
                model.Id = Id.Value;
                model.Name = discount.Name;
                model.Discount_Preceint = discount.Discount_Preceint;
                model.Desc = discount.Desc;
                model.DiscountImage = discount.DiscountImage;
                model.IsActival = discount.IsActival;
            }

            return PartialView("_Action", model);
        }


        [HttpPost]
        public JsonResult Action([Bind(Include = "Id,Name,Desc,Discount_Preceint,DiscountImage,IsActival")]Discount discount , HttpPostedFileBase DicountImage)
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



            if (discount.Id > 0)
            {
                var OldDiscount = _db.discounts.Find(discount.Id);
                string OldDiscontImage = Request.MapPath(OldDiscount.DiscountImage.ToString());

               

                if (Extesion.ToLower() == ".jpg" || Extesion.ToLower() == ".jepg" || Extesion.ToLower() == ".png")
                {
                    if (System.IO.File.Exists(OldDiscontImage))
                    {
                        System.IO.File.Delete(OldDiscontImage);
                    }
                    DicountImage.SaveAs(SavePath);
                    discount.DiscountImage = ImagePath;
                    discount.Create_at = OldDiscount.Create_at;
                    discount.Modified_at = DateTime.Now;

                    Result = discountService.EditDiscount(discount);
                }

            }
            else
            {
             if (Extesion.ToLower() == ".jpg" || Extesion.ToLower() == ".jepg" || Extesion.ToLower() == ".png")
                {
                    DicountImage.SaveAs(SavePath);
                    discount.DiscountImage = ImagePath;
                    discount.Create_at = DateTime.Now;
                    discount.Modified_at = DateTime.Now;
               
                    Result = discountService.SaveDiscount(discount);
                }
            }

   


           

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
       
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = _db.discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", discount);
        }

        // POST: Dashboard/Discounts/Delete/5
        [HttpPost]
        public JsonResult DeleteConfirmed(int id)
        {
            JsonResult json = new JsonResult();
            bool Result = false;

            var discount = discountService.GetDiscount(id);
            string DeleteImage = Request.MapPath(discount.DiscountImage.ToString());

            if (System.IO.File.Exists(DeleteImage))
            {
                System.IO.File.Delete(DeleteImage);
            }

            Result = discountService.DeleteDiscount(discount.Id);


            if (Result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "刪除失敗" };
            }

            return json;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
