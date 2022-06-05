using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceBookStore.Data;
using EcommerceBookStore.Model;
using System.Data.Entity;

namespace EcommerceBookStore.Service
{
    public class CategoriesService
    {
        public IEnumerable<Category> GetAllCategroies()
        {
            var dbContext = new EBookStoreContext();
            return dbContext.Categories.ToList();
        }
        

        public Category  GetCategoryByID(int Id)
        {
            var dbContext = new EBookStoreContext();

            return dbContext.Categories.Find(Id);
        }


        public bool SaveCategory(Category category)
        {
            var dbContext = new EBookStoreContext();
            dbContext.Categories.Add(category);

            return dbContext.SaveChanges() > 0;
        }


        public bool EditCategroy(Category category)
        {
            var dbContext = new EBookStoreContext();
            dbContext.Entry(category).State = EntityState.Modified;
            
            return dbContext.SaveChanges() > 0;
        }

        public bool DeleteCategory()
        {

        }

    }
}
