using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceBookStore.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EcommerceBookStore.Web.Areas.Dashboard.ViewModel
{
    public class UserListViewModel
    {

        [DisplayName("使用者名稱")]
        public string UserName { get; set; }

        [DisplayName("姓")]
        public string FirstName { get; set; }
        [DisplayName("名")]
        public string LastName { get; set; }
        [DisplayName("地址")]
        public string Address { get; set; }
        [DisplayName("電話")]
        public string telephone { get; set; }
        [DisplayName("圖片")]
        public string UserImage { get; set; }
        public IEnumerable<BookStoreUser> Users { get; set; }

    }

    public class UserViewModel
    {
        public string Id { get; set; }
        [DisplayName("使用者名稱")]
        public string UserName { get; set; }
        [DisplayName("信箱")]
        public string Email { get; set; }

        [DisplayName("姓")]
        public string FirstName { get; set; }
        [DisplayName("名")]
        public string LastName { get; set; }
        [DisplayName("地址")]
        public string Address { get; set; }
        [DisplayName("電話")]
        public string telephone { get; set; }
        [DisplayName("圖片")]
        public string UserImage { get; set; }
    }
}