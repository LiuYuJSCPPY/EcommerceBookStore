using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcommerceBookStore.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EcommerceBookStore.Web.Areas.Dashboard.ViewModel
{
    public class UserListViewModel
    {
        public IEnumerable<BookStoreUser> Users { get; set; }

    }

    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string telephone { get; set; }
        public string UserImage { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Modified_at { get; set; }
    }
}