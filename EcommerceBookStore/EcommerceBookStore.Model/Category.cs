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
    [Table("Categroy",Schema ="dbo")]
    public class Category
    {
        public int Id { get; set; }

        [DisplayName("名字")]
        public string Name { get; set; }

        [DisplayName("圖片")]
        public string CategroyImage { get; set; }

        public virtual ICollection<Proudct> proudcts { get; set; }

    }
}
