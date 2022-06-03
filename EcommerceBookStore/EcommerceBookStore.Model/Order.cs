using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcommerceBookStore.Model
{
    [Table("Order",Schema ="dbo")]
    public class Order
    {
        public int Id { get; set; }
        public string BookStoreUserId { get; set; }
        public BookStoreUser BookStoreUser { get; set; }
        public int Total { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Modified_at { get; set; }

        public ICollection<OrderItem> orderItems { get; set; }
    }
}
