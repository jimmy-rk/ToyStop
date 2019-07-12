using EcommerceSite.Entities;
using EcommerceSite.Services;
using EcommerceSite.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceSite.Web.Controllers
{

   
    public class ProductController : Controller
    {

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProductTable(string search, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.PageSize();
            ProductSearchViewModel model = new ProductSearchViewModel();
            model.SearchTerm = search;
            pageNo = pageNo.HasValue ? pageNo.Value>0 ? pageNo.Value : 1 : 1;

            var totalRecords = ProductsService.Instance.GetProductsCount(search);
            model.Products =ProductsService.Instance.GetProducts(search, pageNo.Value,pageSize);
            model.Pager = new Pager(totalRecords, pageNo, pageSize);        
            
            return PartialView(model);
        }
        #region Create
        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();
             model.AvailableCategories = CategoriesService.Instance.GetAllCategories();
            return PartialView(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
           
             var newProduct = new Product();

             newProduct.Name = model.Name;
             newProduct.Description = model.Description;
            newProduct.Stock = model.Stock;
             newProduct.Price = model.Price;
             newProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryID);
             newProduct.ImageURL = model.ImageURL;
             ProductsService.Instance.SaveProduct(newProduct);
             return RedirectToAction("ProductTable");
           
        }
        #endregion


        #region Edit

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = ProductsService.Instance.GetProduct(ID);

            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.Stock = product.Stock;
            model.CategoryID = product.Category != null ? product.Category.ID : 0;
            model.ImageURL = product.ImageURL;

            model.AvailableCategories = CategoriesService.Instance.GetAllCategories();

            return PartialView(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductsService.Instance.GetProduct(model.ID);

            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;
            existingProduct.Stock=model.Stock ;
            existingProduct.Category = null; //mark it null. Because the refernce key is changed below
            existingProduct.CategoryID = model.CategoryID;

            //dont update imageURL if its empty
            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existingProduct.ImageURL = model.ImageURL;
            }

            ProductsService.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }
        #endregion

       
        [HttpGet]
        public ActionResult Details(int ID)
        {
            ProductViewModel model = new ProductViewModel();

            model.Product = ProductsService.Instance.GetProduct(ID);

            return View(model);
        }


        #region Delete
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(Product product)
        {
            ProductsService.Instance.DeleteProduct(product.ID);
            return RedirectToAction("ProductTable");
        }
        #endregion
    }
}