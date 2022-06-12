using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceBookStore.Server;
using EcommerceBookStore.Model;
using EcommerceBookStore.Web.Areas.Dashboard.ViewModel;
using Microsoft.AspNet.Identity.Owin;
using EcommerceBookStore.Data;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace EcommerceBookStore.Web.Areas.Dashboard.Controllers
{
    public class UserController : Controller
    {

        private EBookStoreSignInManager _signInManager;
        private EBookStoreUserManager _userManager;

        public UserController()
        {
        }

        public UserController(EBookStoreUserManager userManager, EBookStoreSignInManager signInManager)
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


        // GET: Dashboard/User
        public ActionResult Index()
        {
            UserListViewModel model = new UserListViewModel();
            model.Users = UserManager.Users.ToList();
            return View(model);
        }



        public ActionResult Action(string Id)
        {
            UserViewModel Edituser = new UserViewModel();
            if(Id != null)
            {
                BookStoreUser OldUser = UserManager.FindById(Id);
                Edituser.FirstName = OldUser.FirstName;
                Edituser.LastName = OldUser.LastName;
                Edituser.Address = OldUser.Address;
                Edituser.telephone = OldUser.telephone;
                Edituser.UserImage = OldUser.UserImage;
               
            }


            return PartialView("_Action");
        }

    }
}