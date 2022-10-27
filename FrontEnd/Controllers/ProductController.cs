using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class ProductController : Controller
    {

        private List<CategoryViewModel> GetCategories()
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/category/");
            response.EnsureSuccessStatusCode();
            //List<Models.CategoryViewModel> categories = new List<Models.CategoryViewModel>();
            var content = response.Content.ReadAsStringAsync().Result;
            List<CategoryViewModel> categories =
                JsonConvert.DeserializeObject<List<CategoryViewModel>>(content);

            return categories;


        }


        private List<SupplierViewModel> GetSuppliers()
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/supplier/");
            response.EnsureSuccessStatusCode();
            //List<Models.CategoryViewModel> categories = new List<Models.CategoryViewModel>();
            var content = response.Content.ReadAsStringAsync().Result;
            List<SupplierViewModel> suppliers =
                JsonConvert.DeserializeObject<List<SupplierViewModel>>(content);

            return suppliers;


        }



        // GET: ProductController
        public ActionResult Index()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/product/");
                response.EnsureSuccessStatusCode();
                //List<Models.CategoryViewModel> categories = new List<Models.CategoryViewModel>();
                var content = response.Content.ReadAsStringAsync().Result;
                List<ProductViewModel> products =
                    JsonConvert.DeserializeObject<List<ProductViewModel>>(content);

                ViewBag.Title = "All ProductViewModel";
                return View(products);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/product/" + id.ToString());
            response.EnsureSuccessStatusCode();
            ProductViewModel ProductViewModel = response.Content.ReadAsAsync<ProductViewModel>().Result;
            //ViewBag.Title = "All Products";
            return View(ProductViewModel);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {

            ProductViewModel product = new ProductViewModel { };
            product.Categories = this.GetCategories();
            product.Suppliers = this.GetSuppliers();


            return View(product);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel product)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PostResponse("api/product", product);
                response.EnsureSuccessStatusCode();
                ProductViewModel productViewModel = response.Content.ReadAsAsync<ProductViewModel>().Result;
                return RedirectToAction("Details", new { id=productViewModel.ProductID});
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/product/" + id.ToString());
            response.EnsureSuccessStatusCode();
            ProductViewModel ProductViewModel = response.Content.ReadAsAsync<ProductViewModel>().Result;

            ProductViewModel.Categories = this.GetCategories();
            ProductViewModel.Suppliers = this.GetSuppliers();
            //ViewBag.Title = "All Products";
            return View(ProductViewModel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel product)
        {
            try
            {

                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PutResponse("api/product", product);
                response.EnsureSuccessStatusCode();
                ProductViewModel productViewModel = response.Content.ReadAsAsync<ProductViewModel>().Result;
                return RedirectToAction("Details", new { id = productViewModel.ProductID });
            }
            catch
            {
                return View();
            }
        }
      
        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/product/" + id.ToString());
            response.EnsureSuccessStatusCode();
            ProductViewModel ProductViewModel = response.Content.ReadAsAsync<ProductViewModel>().Result;

            //ViewBag.Title = "All Products";
            return View(ProductViewModel);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ProductViewModel product)
        {
            try
            {
                int id = product.ProductID;
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.DeleteResponse("api/product/" + id.ToString());
                response.EnsureSuccessStatusCode();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
