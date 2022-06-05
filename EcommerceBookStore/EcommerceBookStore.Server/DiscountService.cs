using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceBookStore.Model;
using EcommerceBookStore.Data;

namespace EcommerceBookStore.Server
{
    public class DiscountService
    {
        public IEnumerable<Discount> GetAllDiscount()
        {
            EBookStoreContext dbContext = new EBookStoreContext();
            return dbContext.discounts.ToList();
        }


        public bool SaveDiscount(Discount discount)
        {
            var dbContext = new EBookStoreContext();
            dbContext.discounts.Add(discount);
            return dbContext.SaveChanges() > 0;
        }
    }
}
