using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceBookStore.Model;
using EcommerceBookStore.Data;
using EcommerceBookStore.Server;
using EcommerceBookStore.Web.ViewModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using PagedList;

namespace EcommerceBookStore.Web.Controllers
{
    public class BookProudctController : Controller
    {

        private EBookStoreContext _db = new EBookStoreContext();
        // GET: BookProudct
        public ActionResult Index()
        {
            BookProudctListView model = new BookProudctListView();
            model.proudcts = _db.proudcts.OrderByDescending(p => p.Id).Take(10).ToList();
            model.categories = _db.Categories.ToList();
            model.discounts = _db.discounts.OrderByDescending(d => d.Id).Take(5).ToList();

            return View(model);
        }

        public ActionResult Shop( string SearchBy,string SearchText,int? CategroyId,int? MinPrice ,int? PageNumber)
        {
            List<Proudct> proudcts = _db.proudcts.ToList();
            BookProudctPagation model = new BookProudctPagation();

            model.categories = _db.Categories.ToList();
            model.discounts = _db.discounts.ToList();

          

            if(SearchBy == "Categroy")
            {
                proudcts = _db.proudcts.Where(x => x.Category.Name == SearchText || SearchText == null).ToList();
            }
            if(SearchBy == "Price")
            {
                int Prices = int.Parse(SearchText);
                proudcts = _db.proudcts.Where(p => p.price < Prices || Prices == null).ToList();
            }
            if(CategroyId > 0)
            {
                proudcts = _db.proudcts.Where(c => c.CategoryId == CategroyId).ToList();
            }
            

            IPagedList<Proudct> proudctsList = proudcts.ToPagedList(PageNumber ?? 1, 9);




            model.proudcts = proudctsList;



            return View(model);
        }

        public ActionResult ShopBookDetail(int Id)
        {
            
            Proudct proudct = _db.proudcts.Find(Id);


            ShopBookDeatil model = new ShopBookDeatil
            {
                proudct = proudct,
                proudctCommits = _db.proudctCommit.Where(m => m.ProudctId == proudct.Id).ToList(),
                proudctImages = _db.proudctImages.Where(Images => Images.ProudctId == proudct.Id).ToList(),
            };

            return View(model);
        }


        [Authorize]
        [HttpPost]
        public JsonResult ShopBookCommit([Bind(Include ="Commit")]ProudctCommit proudctCommit,int Id )
        {
            JsonResult json = new JsonResult();
            bool Result = false;

            if (User.Identity.IsAuthenticated)
            {
                proudctCommit.ProudctId = Id;
                proudctCommit.BookStoreUserId = User.Identity.GetUserId();
                proudctCommit.Create_at = DateTime.Now;
                _db.proudctCommit.Add(proudctCommit);
                Result = _db.SaveChanges() > 0;
            }
           


            if (Result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Error" };
            }

            return json;
        }

    }
}