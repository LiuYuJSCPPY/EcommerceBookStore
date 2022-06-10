using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EcommerceBookStore.Model;
using EcommerceBookStore.Data;
namespace EcommerceBookStore.Server
{
    public class ProudctsService
    {
        public List<Proudct> GetAllProudcts()
        {
            var dbContext = new EBookStoreContext();

            return dbContext.proudcts.Include(model => model.Category).Include(model => model.discount).ToList();
        }

        public Proudct GetProudctsById(int Id)
        {
            var dbContext = new EBookStoreContext();
            return dbContext.proudcts.Find(Id);
        }



       public bool EditProudct(Proudct proudct)
        {
            var dbContext = new EBookStoreContext();
            dbContext.Entry(proudct).State = EntityState.Modified;
            return dbContext.SaveChanges() > 0;
        }

        public bool SaveProudct(Proudct proudct)
        {
            var dbContext = new EBookStoreContext();
            dbContext.proudcts.Add(proudct);
            return dbContext.SaveChanges() > 0;
        }

        public bool DeleteProudct(int? Id)
        {
            var dbContext = new EBookStoreContext();

            Proudct DeleteProudct = dbContext.proudcts.Find(Id);
            dbContext.proudcts.Remove(DeleteProudct);

            return dbContext.SaveChanges() > 0;

        }

    }
}
