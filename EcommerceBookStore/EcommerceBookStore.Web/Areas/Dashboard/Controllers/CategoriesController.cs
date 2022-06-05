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
        private EBookStoreContext _db = new EBookStoreContext();
        CategoriesService categoriesService = new CategoriesService();
        // GET: Dashboard/Categories
        public ActionResult Index()
        {
            CategoriesService categoriesService = new CategoriesService();
            return View(categoriesService.GetAllCategroies());
        }

        // GET: Dashboard/Categories/Details/5

        public ActionResult Action(int? Id)
        {
            CategoryViewModel model = new CategoryViewModel();
            if (Id.HasValue)
            {
                var EditCategory = _db.Categories.Find(Id);
                model.Id = Id.Value;
                model.Name = EditCategory.Name;
                model.CategroyImage = EditCategory.CategroyImage;
            }


            return PartialView("_Action",model);
        }

        [HttpPost]
        public JsonResult Action([Bind(Include = "Id,Name")] Category category, HttpPostedFileBase UpImage)
        {
            JsonResult json = new JsonResult();
            bool Result = false;

            string SaveFilePath = Server.MapPath("~/Image/Category/");
            string FileName = Path.GetFileName(UpImage.FileName);
            string _FileName = DateTime.Now.ToString("yyyymmssfff") + FileName;
            string Extension = Path.GetExtension(UpImage.FileName);
            string SavePath = Path.Combine(SaveFilePath, _FileName);
            string CategroyImage = "~/Image/Category/" + _FileName;


            if (category.Id > 0)
            {
                var OldCategory = _db.Categories.Find(category.Id);
                string OldImage = Request.MapPath(OldCategory.CategroyImage.ToString());

                var EditCategory = categoriesService.GetCategoryByID(category.Id);




                EditCategory.Id = category.Id;
                EditCategory.Name = category.Name;
                EditCategory.CategroyImage = CategroyImage;

                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".jepg" || Extension.ToLower() == ".png")
                {
                    if (UpImage.ContentLength < 100000000)
                    {
                        if (System.IO.File.Exists(OldImage))
                        {
                            System.IO.File.Delete(OldImage);
                            UpImage.SaveAs(SavePath);
                            Result = categoriesService.EditCategroy(EditCategory);
                        }

                    }

                }
            }
            else
            {

                if (!Directory.Exists(SaveFilePath))
                {
                    Directory.CreateDirectory(SaveFilePath);
                }


                if (Extension.ToLower() == ".jpg" || Extension.ToLower() == ".jepg" || Extension.ToLower() == ".png")
                {
                    if (UpImage.ContentLength < 100000000)
                    {
                        UpImage.SaveAs(SavePath);
                        category.CategroyImage = CategroyImage;
                        Result = categoriesService.SaveCategory(category);
                    }
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

        // GET: Dashboard/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _db.Categories.Find(id);
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
            Category category = _db.Categories.Find(id);
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
