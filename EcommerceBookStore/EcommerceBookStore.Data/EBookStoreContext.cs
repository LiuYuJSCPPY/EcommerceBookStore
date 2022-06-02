using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceBookStore.Model;

namespace EcommerceBookStore.Data
{
    public class EBookStoreContext : IdentityDbContext<BookStoreUser>
    {
        public EBookStoreContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static EBookStoreContext Create()
        {
            return new EBookStoreContext();
        }
    }
}
