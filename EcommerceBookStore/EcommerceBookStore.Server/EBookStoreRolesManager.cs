using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using EcommerceBookStore.Data;
using Microsoft.Owin;



namespace EcommerceBookStore.Server
{
    public class EBookStoreRolesManager : RoleManager<IdentityRole>
    {
        public EBookStoreRolesManager(IRoleStore<IdentityRole,string>roleStore) : base(roleStore)
        {

        }
        public static EBookStoreRolesManager Create(IdentityFactoryOptions<EBookStoreRolesManager> options,IOwinContext context)
        {
            return new EBookStoreRolesManager(new RoleStore<IdentityRole>(context.Get<EBookStoreContext>()));
        }
    }
}
