using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceBookStore.Web.ViewModel;
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
        private EBookStoreRolesManager _roleManager;

        public RoleController()
        {
        }

        public RoleController(EBookStoreUserManager userManager, EBookStoreSignInManager signInManager, EBookStoreRolesManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RolesManager = roleManager;
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
        public EBookStoreRolesManager RolesManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<EBookStoreRolesManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }


        // GET: Dashboard/Role
        public ActionResult Index()
        {
            RoleListingModel model = new RoleListingModel();
            var ListRole = RolesManager.Roles.AsQueryable();
            model.roles = ListRole.ToList();
            return View(model);
        }

        public ActionResult Action(string Id)
        {
            RoleViewModel model = new RoleViewModel();

            if (!string.IsNullOrEmpty(Id))
            {
                var role = RolesManager.FindById(Id);
                model.name = role.Name;
                model.Id = role.Id;
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Action(RoleViewModel roleViewModel)
        {
            JsonResult json = new JsonResult();

            IdentityResult Result = null;

            if (!string.IsNullOrEmpty(roleViewModel.Id))
            {
                IdentityRole role = RolesManager.FindById(roleViewModel.Id);
                role.Name = roleViewModel.name;

                Result = RolesManager.Update(role);
            }
            else
            {
                var role = new IdentityRole();
                role.Name = roleViewModel.name;
                Result = RolesManager.Create(role);
            }
            json.Data = new { Success = Result.Succeeded, Message = $"{Result.Errors}" };

            return json;
        }
    }
}