using EcommerceBookStore.Model;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceBookStore.Server
{
    public class EBookStoreSignInManager : SignInManager<BookStoreUser, string>
    {
        public EBookStoreSignInManager(EBookStoreUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(BookStoreUser user)
        {
            return user.GenerateUserIdentityAsync((EBookStoreUserManager)UserManager);
        }

        public static EBookStoreSignInManager Create(IdentityFactoryOptions<EBookStoreSignInManager> options, IOwinContext context)
        {
            return new EBookStoreSignInManager(context.GetUserManager<EBookStoreUserManager>(), context.Authentication);
        }
    }
    
}
