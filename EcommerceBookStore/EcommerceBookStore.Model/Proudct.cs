using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EcommerceBookStore.Model
{
    [Table("Proudct",Schema ="dbo")]
    public class Proudct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string PushlingHouse { get; set; }

        [DisplayFormat(DataFormatString ="{0:d yyyy-mm-dd}")]
        public DateTime PubshDate { get; set; }
        public string desc { get; set; }
        public int ProudctInventory { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int price { get; set; }
        public int? DiscountId { get; set; }
        public Discount discount { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Modified_at { get; set; }
        public string ProudctImage { get; set; }




        public ICollection<CartItem> cartItems { get; set; }
        public ICollection<OrderItem> orderItems { get; set; }

    }
}
