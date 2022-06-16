using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace EcommerceBookStore.Model
{
    [Table("OrderItem",Schema ="dbo")]
    public class OrderItem
    {
        public int Id { get; set; } 
        public int OrderId { get; set; }
        public Order order { get; set; }

        public int ProudctId { get; set; }
        public Proudct proudct { get; set; }

     


    }
}
