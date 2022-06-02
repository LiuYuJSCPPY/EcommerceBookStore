using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceBookStore.Model
{
    public class BookStoreUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get;set; }
        public string telephone { get; set; }
        public string UserImage { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Modified_at { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<BookStoreUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

}
