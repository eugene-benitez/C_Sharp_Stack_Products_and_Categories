using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Products_And_Categories.Models;

namespace Products_And_Categories.Controllers
{
    public class HomeController : Controller
    {

        private MyContext dbContext;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }


        public IActionResult Index()
        {
            DateTime CurrentTime = DateTime.Now;
            ViewBag.Now = CurrentTime;
            List<Products> AllProducts = dbContext.Products
                        .ToList();
            @ViewBag.AllProducts = AllProducts;
            return View();
        }


        [HttpGet("categories")]
        public IActionResult Categories()
        {
            DateTime CurrentTime = DateTime.Now;
            ViewBag.Now = CurrentTime;
            List<Categories> AllCategories = dbContext.Categories
                        .ToList();
            @ViewBag.AllCategories = AllCategories;
            return View();
        }

        [HttpGet("products/{productId}")]
        public IActionResult ProductProfile(int productId)
        {
            DateTime CurrentTime = DateTime.Now;
            ViewBag.Now = CurrentTime;
            Products retrievedProduct = dbContext.Products.SingleOrDefault(u => u.ProductId == productId);
            @ViewBag.retrievedProduct = retrievedProduct;

            List<Categories> AllCategories = dbContext.Categories
            .ToList();
            @ViewBag.AllCategories = AllCategories;


            AddCategoryToProductView newCategoryToProduct = new AddCategoryToProductView();
            newCategoryToProduct.Categories = dbContext.Categories
                 .Include(c => c.Associations)
                 .ThenInclude(a => a.Products)
                 .ToList();
            newCategoryToProduct.Products = dbContext.Products
            .Where(p => p.ProductId == productId)
            .FirstOrDefault();


            var productWithCategoriesAndAssociations = dbContext.Products
            .Include(product => product.Associated)
            .ThenInclude(sub => sub.Categories)
            .FirstOrDefault(product => product.ProductId == productId);

            @ViewBag.productCategories = productWithCategoriesAndAssociations;
            return View(newCategoryToProduct);
        }


        [HttpGet("category/{categoryId}")]
        public IActionResult CategoryProfile(int categoryId)
        {
            List<Products> AllProducts = dbContext.Products
            .ToList();
            @ViewBag.AllProducts = AllProducts;

            DateTime CurrentTime = DateTime.Now;
            ViewBag.Now = CurrentTime;


            Categories retrievedCategory = dbContext.Categories.SingleOrDefault(u => u.CategoriesId == categoryId);
            @ViewBag.retrievedCategory = retrievedCategory;



            AddProductToCategoryView newCategoryToProduct = new AddProductToCategoryView();
            newCategoryToProduct.Products = dbContext.Products
                 .Include(c => c.Associated)
                 .ThenInclude(a => a.Categories)
                 .ToList();
            newCategoryToProduct.Categories = dbContext.Categories
            .Where(p => p.CategoriesId == categoryId)
            .FirstOrDefault();


            // AddProductToCategoryView newProductToCategory = new AddProductToCategoryView();


            // // Categories thisCategory = dbContext.Categories
            // //     .Where(c => c.CategoriesId == categoryId)
            // //     .Include(c => c.Associations)
            // //     .ThenInclude(assoc => assoc.Products)
            // //     .FirstOrDefault();

            // // newProductToCategory.Products = dbContext.Products
            // //     .Include(p => p.Associated).ToList();

            // // @ViewBag.AddProdToCategoryView = newProductToCategory;

            var categoryWithProducts = dbContext.Categories
            .Include(category => category.Associations)
            .ThenInclude(sub => sub.Products)
            .FirstOrDefault(category => category.CategoriesId == categoryId);

            @ViewBag.categoryWithProducts = categoryWithProducts;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        //CREATE STATEMENTS



        [HttpPost("createProduct")]
        public IActionResult CreateProduct(Products NewProduct)
        {
            dbContext.Add(NewProduct);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost("createCategory")]
        public IActionResult CreateCategory(Categories NewCategory)
        {
            dbContext.Add(NewCategory);
            dbContext.SaveChanges();
            return RedirectToAction("Categories");
        }

        [HttpPost("addAssociation")]
        public IActionResult AddAssociation(Associations NewAssociation)
        {
            dbContext.Add(NewAssociation);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
