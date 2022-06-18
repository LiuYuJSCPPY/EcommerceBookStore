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
using EcommerceBookStore.Server;
using EcommerceBookStore.Web.Areas.Dashboard.ViewModel;
using System.IO;

namespace EcommerceBookStore.Web.Areas.Dashboard.Controllers
{
    public class ProudctsController : Controller
    {
        private EBookStoreContext db = new EBookStoreContext();
        private ProudctsService proudctsService = new ProudctsService();
        // GET: Dashboard/Proudcts
        public ActionResult Index()
        {
            ProudctListViewModel ProuctsListModel = new ProudctListViewModel();
            ProuctsListModel.proudcts = proudctsService.GetAllProudcts();
            return View(ProuctsListModel);
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


        public ActionResult Action(int? Id)
        {
            ProudctViewModel model = new ProudctViewModel();

          

            if(Id.HasValue)
            {  
                var EditProudct = proudctsService.GetProudctsById(Id.Value);
                model.Id = EditProudct.Id;
                model.Name = EditProudct.Name;
                model.Author = EditProudct.Author;
                model.PushlingHouse = EditProudct.PushlingHouse;
                model.PubshDate = EditProudct.PubshDate;
                model.desc = EditProudct.desc;
                model.ProudctInventory = EditProudct.ProudctInventory;
                model.CategoryId = EditProudct.CategoryId;
                model.price = EditProudct.price;
                model.DiscountId = EditProudct.DiscountId;
                model.ProudctImage = EditProudct.ProudctImage;
            }

            model.Category = db.Categories.Select(Category=> new SelectListItem
            {
                Text = Category.Name,
                Value = Category.Id.ToString()
            }).ToList();

            model.Discount = db.discounts.Select(Discount => new SelectListItem
            {
                Text = Discount.Name,
                Value = Discount.Id.ToString()
            }).ToList();


            return PartialView("_Action",model);
        }

        [HttpPost]
        public JsonResult Action([Bind(Include = "Id,Name,Author,PushlingHouse,PubshDate,desc,ProudctInventory,CategoryId,price,DiscountId")]Proudct proudct ,HttpPostedFileBase ProudctImage,int? Id)
        {
            JsonResult json = new JsonResult();
            bool Result = false;

            string FilePath = Server.MapPath("~/Image/Proudct/");
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            string FileName = Path.GetFileName(ProudctImage.FileName);
            string _FileName = DateTime.Now.ToString("yyyymmssfff") + FileName;
            string Extesion = Path.GetExtension(ProudctImage.FileName);
            string SaveFilePath = Path.Combine(FilePath, _FileName);
            string DbSaveFilePath = "~/Image/Proudct/" + _FileName;



            if (Id.Value > 0)
            {
                var dbEidtProudct = proudctsService.GetProudctsById(Id.Value);
                var EditImage = Request.MapPath(dbEidtProudct.ProudctImage.ToString());


                if (System.IO.File.Exists(EditImage))
                {
                    System.IO.File.Delete(EditImage);
                    ProudctImage.SaveAs(SaveFilePath);
                    proudct.ProudctImage = DbSaveFilePath;
                    proudct.Create_at = dbEidtProudct.Create_at;
                    proudct.Modified_at = DateTime.Now;
                    Result = proudctsService.EditProudct(proudct);
                }
            }
            else
            {
                if(Extesion.ToLower() == ".jpg" || Extesion.ToLower() == ".jepg" || Extesion.ToLower() == ".png")
                {
                    ProudctImage.SaveAs(SaveFilePath);
                    proudct.ProudctImage = DbSaveFilePath;
                    proudct.Create_at = DateTime.Now;
                    proudct.Modified_at = DateTime.Now;
                    Result = proudctsService.SaveProudct(proudct);
                
                }
            }

            


            if (Result)
            {
                json.Data = new {Success = true};
            }
            else
            {
                json.Data = new { Success = false, Message = "上傳失敗" };
            }


            return json;
        }




      
        // GET: Dashboard/Proudcts/Delete/5
        public ActionResult Delete(int Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proudct DeleteProudct = proudctsService.GetProudctsById(Id);
            if (DeleteProudct == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", DeleteProudct);
        }

        // POST: Dashboard/Proudcts/Delete/5
        [HttpPost]
        public JsonResult DeleteConfirmed(int Id)
        {
            JsonResult json = new JsonResult();
            bool Result = false;
            Proudct proudct = db.proudcts.Find(Id);
            if(proudct.ProudctImage != null)
            {
                string DeleteImage = Request.MapPath(proudct.ProudctImage.ToString());
                if (System.IO.File.Exists(DeleteImage))
                {
                    System.IO.File.Delete(DeleteImage);
                }
            }

            Result = proudctsService.DeleteProudct(Id);

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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
