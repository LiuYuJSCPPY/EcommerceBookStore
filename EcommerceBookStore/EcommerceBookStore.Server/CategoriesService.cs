using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceBookStore.Data;
using EcommerceBookStore.Model;

namespace EcommerceBookStore.Service
{
    public class CategoriesService
    {
        public IEnumerable<Category> GetAllCategroies()
        {
            var dbContext = new EBookStoreContext();
            return dbContext.Categories.ToList();
        }
        

        public bool SaveCategory(Category category)
        {
            var dbContext = new EBookStoreContext();
            dbContext.Categories.Add(category);

            return dbContext.SaveChanges() > 0;
        }


    }
}
