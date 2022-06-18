using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceBookStore.Model;
using EcommerceBookStore.Data;
using EcommerceBookStore.Server;
using EcommerceBookStore.Web.ViewModel;


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
    }
}