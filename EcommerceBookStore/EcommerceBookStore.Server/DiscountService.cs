using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceBookStore.Model;
using EcommerceBookStore.Data;
using System.Data.Entity;


namespace EcommerceBookStore.Server
{
    public class DiscountService
    {
        public IEnumerable<Discount> GetAllDiscount()
        {
            EBookStoreContext dbContext = new EBookStoreContext();
            return dbContext.discounts.ToList();
        }

        public Discount GetDiscount(int Id)
        {
            EBookStoreContext dbContext = new EBookStoreContext();
            return dbContext.discounts.Find(Id);
        }



        public bool SaveDiscount(Discount discount)
        {
            var dbContext = new EBookStoreContext();
            dbContext.discounts.Add(discount);
            return dbContext.SaveChanges() > 0;
        }

        public bool EditDiscount(Discount discount)
        {
            var dbContext = new EBookStoreContext();
            dbContext.Entry(discount).State = EntityState.Modified;
            return dbContext.SaveChanges() > 0;
        }

        public bool DeleteDiscount(int Id)
        {
            var dbContext = new EBookStoreContext();
            var DeleteModel = dbContext.discounts.Find(Id);
            dbContext.discounts.Remove(DeleteModel);
            return dbContext.SaveChanges() > 0;
        }
    }
}
