using EcommerceBookStore.Data;
using EcommerceBookStore.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceBookStore.Server
{
    public class EBookStoreUserManager : UserManager<BookStoreUser>
    {
        public EBookStoreUserManager(IUserStore<BookStoreUser> store)
            : base(store)
        {
        }

        public static EBookStoreUserManager Create(IdentityFactoryOptions<EBookStoreUserManager> options, IOwinContext context)
        {
            var manager = new EBookStoreUserManager(new UserStore<BookStoreUser>(context.Get<EBookStoreContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<BookStoreUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<BookStoreUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<BookStoreUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });

            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<BookStoreUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
   
}
