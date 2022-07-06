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
    [Table("CartItem",Schema ="dbo")]
    public class CartItem
    {
        public int Id { get; set; }

        public int CartId { get; set; }
        public virtual Cart cart { get; set; }
        
        public int ProudctId { get; set; }
        public virtual Proudct proudct { get; set; }

        public int quantity { get; set; }

        
    }
}
