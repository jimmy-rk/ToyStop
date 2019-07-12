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
    public class ProductsService
    {
        #region Singleton
        public static ProductsService Instance
        {
            get
            {
                if (instance == null) instance = new ProductsService();
                return instance;
            }
        }
        private static ProductsService instance { get; set; }

        private ProductsService()
        {

        }

        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy,int pageNo,int pageSize)
        {
            using(var context=new ESContext())
            {
                var products = context.Products.ToList();
                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.CategoryID == categoryID.Value).ToList();
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products =products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price>=minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice +1 ).ToList();
                }
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                       
                        case 2:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        case 4:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                           products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                    }

                }

                return products.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        #endregion

        public int SearchProductsCount(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {
            using (var context = new ESContext())
            {
                var products = context.Products.ToList();
                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.CategoryID == categoryID.Value).ToList();
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice + 1).ToList();
                }
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {

                        case 2:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        case 4:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                    }

                }

                return (products.Count );
            }
        }
        public List<Product> GetProducts(string search, int pageNo,int pageSize)
        {
              // int.Parse( ConfigurationsService.Instance.GetConfig("ListingPageSize").Value);
            using (var context = new ESContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(p => p.Name != null &&
                    p.Name.ToLower().Contains(search.ToLower()))
                    .OrderBy(x => x.ID)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Category)
                    .ToList();
                }
                else
                {
                    return context.Products
                        .OrderBy(x => x.ID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Category)
                        .ToList();
                }
            }
        }

        public void UpdateProductStock(int pid)
        {
            using(var context =new ESContext())
            {
                var product = context.Products.Where(p => p.ID == pid).FirstOrDefault();
                product.Stock--;
                context.SaveChanges();
            }
        }

        public int GetProductsCount(string search)
        {
              // int.Parse( ConfigurationsService.Instance.GetConfig("ListingPageSize").Value);
            using (var context = new ESContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Products.Where(p => p.Name != null &&
                    p.Name.ToLower().Contains(search.ToLower()))
                    .Count();
                }
                else
                {
                    return context.Products.Count();
                        
                }
            }
        }
        public List<Product> GetProducts(int pageNo)
        {
            int pageSize = 5;
            using (var context = new ESContext())
            {
                return context.Products.OrderBy(x=>x.ID).Skip((pageNo-1)*pageSize).Take(pageSize).Include(p => p.Category).ToList();
                //return context.Products.Include(p => p.Category).ToList();
            }
        }

        public List<Product> GetProductsByCategory(int categoryID,int pageSize)
        {
           
            using (var context = new ESContext())
            {
                return context.Products.Where(p=>p.Category.ID==categoryID).OrderByDescending(x=>x.ID).Take(pageSize).Include(p => p.Category).ToList();
              
            }
        }

        public List<Product> GetProducts(int pageNo,int pageSize)
        {
           
            using (var context = new ESContext())
            {
                return context.Products.OrderByDescending(x=>x.ID).Skip((pageNo-1)*pageSize).Take(pageSize).Include(p => p.Category).ToList();
              
            }
        }

        public List<Product> GetLatestProducts(int numberOfProducts)
        {
           
            using (var context = new ESContext())
            {
                return context.Products.OrderByDescending(x=>x.ID).Take(numberOfProducts).Include(p => p.Category).ToList();
                //return context.Products.Include(p => p.Category).ToList();
            }
        }
        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new ESContext())
            {
                return context.Products.Where(product => IDs.Contains(product.ID)).ToList();

            }
        }
        public Product GetProduct(int ID)
        {
            using (var context = new ESContext())
            {
                return context.Products.Where(x => x.ID == ID).Include(x => x.Category).FirstOrDefault();

            }
        }
        public void SaveProduct(Product product)
        {
            using(var context=new ESContext())
            {
                context.Entry(product.Category).State = EntityState.Unchanged;
                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        public void UpdateProduct(Product product)
        {
            using(var context=new ESContext())
            {
                context.Entry(product).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteProduct(int ID)
        {
            using(var context=new ESContext())
            {
                var product = context.Products.Find(ID);
                context.Entry(product).State = EntityState.Deleted;
                //context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        public int GetMaximumPrice()
        {
            using(var context =new ESContext())
            {
                return (int)(context.Products.Max(p => p.Price));
            }

        }
       
    }
}
