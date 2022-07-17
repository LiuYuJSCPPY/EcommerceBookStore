using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceBookStore.Server;
using EcommerceBookStore.Model;
using EcommerceBookStore.Web.Areas.Dashboard.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using EcommerceBookStore.Data;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.IO;

namespace EcommerceBookStore.Web.Areas.Dashboard.Controllers
{
    public class UserController : Controller
    {

        private EBookStoreSignInManager _signInManager;
        private EBookStoreUserManager _userManager;
        private EBookStoreRolesManager _roleManager;
        public UserController()
        {
        }

        public UserController(EBookStoreUserManager userManager, EBookStoreSignInManager signInManager,EBookStoreRolesManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
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
        public EBookStoreRolesManager RoleManager
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
                Edituser.UserName = OldUser.UserName;
                Edituser.Email = OldUser.Email;
                Edituser.Id = OldUser.Id;
            }


            return PartialView("_Action",Edituser);

        }


        [HttpPost]
        public JsonResult Action(BookStoreUser bookStoreUser ,HttpPostedFileBase UserImage)
        {
            JsonResult json = new JsonResult();
            IdentityResult result = null;

            string FileUPathImage = Server.MapPath("~/Image/User/");
            string FileUserName = Path.GetFileName(UserImage.FileName);
            string _FileUserName = DateTime.Now.ToString("yyyymmssfff") + FileUserName;
            string FileUSaveImage = Path.Combine(FileUPathImage, _FileUserName);

            if (!string.IsNullOrEmpty(bookStoreUser.Id))
            {
               

                var user  = UserManager.FindById(bookStoreUser.Id);

                if (user.UserImage != null)
                {
                    string FileHImage = Request.MapPath(user.UserImage);
                    if (System.IO.File.Exists(FileHImage))
                    {
                        System.IO.File.Delete(FileHImage);
                        UserImage.SaveAs(FileUSaveImage);
                    }
                }
                else
                {
                    UserImage.SaveAs(FileUSaveImage);
                }

                user.Id = bookStoreUser.Id;
                user.FirstName = bookStoreUser.FirstName;
                user.LastName = bookStoreUser.LastName;
                user.Email = bookStoreUser.Email;
                user.Address = bookStoreUser.Address;
                user.telephone = bookStoreUser.telephone;
                user.UserName = bookStoreUser.UserName;
                user.UserImage = "~/Image/User/" + _FileUserName;

                result = UserManager.Update(user);
            }
            else
            {
                var user = new BookStoreUser();
                UserImage.SaveAs(FileUSaveImage);
                user.FirstName = bookStoreUser.FirstName;
                user.LastName = bookStoreUser.LastName;
                user.Address = bookStoreUser.Address;
                user.Email = bookStoreUser.Email;
                user.telephone = bookStoreUser.telephone;
                user.UserName = bookStoreUser.UserName;
                user.UserImage = "~/Image/User/" + _FileUserName;

                result = UserManager.Create(user);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            return json;
        }

        public ActionResult Delete(string Id)
        {

            UserViewModel model = new UserViewModel();
            if (Id != null)
            {
                BookStoreUser OldUser = UserManager.FindById(Id);
                model.FirstName = OldUser.FirstName;
                model.LastName = OldUser.LastName;
                model.Address = OldUser.Address;
                model.telephone = OldUser.telephone;
                model.UserImage = OldUser.UserImage;
                model.UserName = OldUser.UserName;
                model.Email = OldUser.Email;
                model.Id = OldUser.Id;
            }
            return PartialView("_Delete",model);
        }


        public ActionResult UserRole(string Id)
        {
            UserRoleViewModel model = new UserRoleViewModel();
            model.UserId = Id;
            var User = UserManager.FindById(Id);
            var UserRole = User.Roles.Select(x => x.RoleId).ToList();
            model.UserRole = RoleManager.Roles.Where(x => UserRole.Contains(x.Id)).ToList();
            model.Roles = RoleManager.Roles.Where(x => !UserRole.Contains(x.Id)).ToList();

            return PartialView("_UserRole",model);
        }

        [HttpPost]
        public JsonResult UserRoleOpdestion(string Id,string RoleId , bool IsDelete = false)
        {
            JsonResult json = new JsonResult();

            var User = UserManager.FindById(Id);
            var Role = RoleManager.FindById(RoleId);

            if(User != null && Role != null)
            {
                IdentityResult result = null;
                if (!IsDelete)
                {
                    result = UserManager.AddToRole(Id, Role.Name);
                }
                else
                {
                    result = UserManager.RemoveFromRole(Id, Role.Name);
                }
                json.Data = new { Success = result.Succeeded, Message = $"{result.Errors}" };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid operation." };
            }

            return json;
        }
    }
}