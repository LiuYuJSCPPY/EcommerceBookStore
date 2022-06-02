using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EcommerceBookStore.Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class applicationuser : identityuser
    //{
    //    public async task<claimsidentity> generateuseridentityasync(usermanager<applicationuser> manager)
    //    {
    //        // note the authenticationtype must match the one defined in cookieauthenticationoptions.authenticationtype
    //        var useridentity = await manager.createidentityasync(this, defaultauthenticationtypes.applicationcookie);
    //        // add custom user claims here
    //        return useridentity;
    //    }
    //}

    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext()
    //        : base("DefaultConnection", throwIfV1Schema: false)
    //    {
    //    }

    //    public static ApplicationDbContext Create()
    //    {
    //        return new ApplicationDbContext();
    //    }
    //}
}