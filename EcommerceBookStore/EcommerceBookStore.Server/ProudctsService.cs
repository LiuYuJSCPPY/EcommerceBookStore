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
        public IEnumerable<Proudct> GetAllProudcts()
        {
            var dbContext = new EBookStoreContext();

            return dbContext.proudcts.Include(model => model.Category).Include(model => model.discount).ToList();
        }
    }
}
