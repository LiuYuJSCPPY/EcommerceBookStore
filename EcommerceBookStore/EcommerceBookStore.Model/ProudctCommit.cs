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
    [Table("ProudctCommit",Schema ="dbo")]
    public class ProudctCommit
    {
        public int Id { get; set; }
        public int ProudctId { get; set; }
        public virtual Proudct proudct { get; set; }
        public string BookStoreUserId { get; set; }
        public virtual BookStoreUser BookStoreUser { get; set; }
        public string Commit { get; set; }
        public DateTime Create_at { get; set; }

    }
}
