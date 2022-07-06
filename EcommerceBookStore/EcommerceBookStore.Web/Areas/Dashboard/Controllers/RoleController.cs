using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using EcommerceBookStore.Model;
using EcommerceBookStore.Data;
using EcommerceBookStore.Server;
using EcommerceBookStore.Web.Areas.Dashboard.ViewModel;

namespace EcommerceBookStore.Web.Areas.Dashboard.Controllers
{
    public class RoleController : Controller
    {
        private EBookStoreSignInManager _signInManager;
        private EBookStoreUserManager _userManager;

        public RoleController()
        {
        }

        public RoleController(EBookStoreUserManager userManager, EBookStoreSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public EBookStoreSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<EBookStoreSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public EBookStoreUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<EBookStoreUserManager>();
            }
            private set
            {
                _userManager = value;
            }

        }



        // GET: Dashboard/Role
        public ActionResult Index()
        {
            return View();
        }
    }
}