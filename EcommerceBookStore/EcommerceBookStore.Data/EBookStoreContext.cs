using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceBookStore.Model;
using System.Data.Entity;

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

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> discounts { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Proudct> proudcts { get; set; }
        
    }
}
