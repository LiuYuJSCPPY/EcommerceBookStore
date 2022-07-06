using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceBookStore.Model
{
    [Table("Cart",Schema ="dbo")]
    public class Cart
    {
        public int Id { get; set; }
        public string BookStoreUserId { get; set; }
        public virtual BookStoreUser BookStoreUser { get; set; }

        public int? Total { get; set; }
       

        public virtual ICollection<CartItem> cartItems  { get; set; }
    }
}
