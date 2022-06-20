using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceBookStore.Model;
using EcommerceBookStore.Data;
using EcommerceBookStore.Server;
using EcommerceBookStore.Web.ViewModel;
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
            if(SearchBy == "Categroy")
            {
                proudcts = _db.proudcts.Where(x => x.Category.Name == SearchText || SearchText == null).ToList();
            }
            if(SearchBy == "Price")
            {
                proudcts = _db.proudcts.Where(p => p.price < MinPrice || MinPrice == null).ToList();
            }
            if(CategroyId > 0)
            {
                proudcts = _db.proudcts.Where(c => c.CategoryId == CategroyId).ToList();
            }
            

            IPagedList<Proudct> proudctsList = proudcts.ToPagedList(PageNumber ?? 1, 9);



            
            model.proudcts = proudctsList;
            model.categories = _db.Categories.ToList();
            model.discounts = _db.discounts.ToList();

            
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

    }
}