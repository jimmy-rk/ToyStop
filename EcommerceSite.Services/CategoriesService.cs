using EcommerceSite.Database;
using EcommerceSite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EcommerceSite.Services
{
    public class CategoriesService
    {

        #region Singleton
        public static CategoriesService Instance
        {
            get
            {
                if (instance == null) instance = new CategoriesService();
                return instance;
            }
        }
        private static CategoriesService instance { get; set; }

        private CategoriesService()
        {
        }

        #endregion


        public List<Category> GetCategories(string search,int pageNo)
        {
            int pageSize = 20;  // int.Parse( ConfigurationsService.Instance.GetConfig("ListingPageSize").Value);
            using (var context = new ESContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(c => c.Name != null && 
                    c.Name.ToLower().Contains(search.ToLower()))
                    .OrderBy(x => x.ID)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Products)
                    .ToList();
                }
                else
                {
                    return context.Categories
                        .OrderBy(x => x.ID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }
            }
        }
        public List<Category> GetAllCategories()
        {
           
            using (var context = new ESContext())
            {
                    return context.Categories.ToList();
           
            }
        }
        public int GetCategoriesCount(string search)
        {
            using (var context = new ESContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(c => c.Name != null &&
                    c.Name.ToLower().Contains(search.ToLower())).Count();
                    
                }
                else
                {
                    return context.Categories.Count();
                }
               

            }
        }

        public List<Category> GetFeaturedCategories()
        {
            using (var context = new ESContext())
            {
                return context.Categories.Where(x=>x.isFeatured).ToList();

            }
        }
        public Category GetCategory(int ID)
        {
            using (var context = new ESContext())
            {
                return context.Categories.Find(ID);

            }
        }
        public void SaveCategory(Category category)
        {
            using(var context=new ESContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
        public void UpdateCategory(Category category)
        {
            using(var context=new ESContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteCategory(int ID)
        {
            using(var context=new ESContext())
            {
                var category = context.Categories.Find(ID);
                context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                //context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
       
    }
}
