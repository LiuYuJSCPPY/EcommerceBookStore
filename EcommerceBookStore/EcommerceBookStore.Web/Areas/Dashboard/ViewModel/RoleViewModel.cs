using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
namespace EcommerceBookStore.Web.Areas.Dashboard.ViewModel
{
    public class RoleListingModel
    {
        public IEnumerable<IdentityRole> roles { get; set; }
    }
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string name { get; set; }

    }
}