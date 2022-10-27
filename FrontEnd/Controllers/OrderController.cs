using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class OrderController : Controller
    {
        private List<EmployeeViewModel> GetEmployees()
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/employee/");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            List<EmployeeViewModel> employees =
                JsonConvert.DeserializeObject<List<EmployeeViewModel>>(content);
            return employees;
        }


        private List<CustomerViewModel> GetCustomers()
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/customer/");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            List<CustomerViewModel> customers =
                JsonConvert.DeserializeObject<List<CustomerViewModel>>(content);
            return customers;
        }

        private List<ShipperViewModel> GetShippers()
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/shipper/");
            response.EnsureSuccessStatusCode();
            var content = response.Content.ReadAsStringAsync().Result;
            List<ShipperViewModel> shippers =
                JsonConvert.DeserializeObject<List<ShipperViewModel>>(content);

            return shippers;


        }

        // GET: ProductController
        public ActionResult Index()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/order/");
                response.EnsureSuccessStatusCode();
                var content = response.Content.ReadAsStringAsync().Result;
                List<OrderViewModel> orders =
                    JsonConvert.DeserializeObject<List<OrderViewModel>>(content);
                ViewBag.Title = "All OrderViewModel";
                return View(orders);
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
            HttpResponseMessage response = serviceObj.GetResponse("api/order/" + id.ToString());
            response.EnsureSuccessStatusCode();
            OrderViewModel OrderViewModel = response.Content.ReadAsAsync<OrderViewModel>().Result;
            return View(OrderViewModel);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {

            OrderViewModel product = new OrderViewModel { };
            product.Customers = this.GetCustomers();
            product.Employees = this.GetEmployees();
            product.Shippers = this.GetShippers();

            return View(product);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel order)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PostResponse("api/order",order);
                response.EnsureSuccessStatusCode();
                OrderViewModel orderViewModel = response.Content.ReadAsAsync<OrderViewModel>().Result;
                return RedirectToAction("Details", new { id = orderViewModel.OrderId });
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
            HttpResponseMessage response = serviceObj.GetResponse("api/order/" + id.ToString());
            response.EnsureSuccessStatusCode();
            OrderViewModel orderViewModel = response.Content.ReadAsAsync<OrderViewModel>().Result;

            orderViewModel.Customers = this.GetCustomers();
            orderViewModel.Employees = this.GetEmployees();
            orderViewModel.Shippers = this.GetShippers();

            return View(orderViewModel);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OrderViewModel order)
        {
            try
            {

                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PutResponse("api/order", order);
                response.EnsureSuccessStatusCode();
                OrderViewModel orderViewModel = response.Content.ReadAsAsync<OrderViewModel>().Result;
                return RedirectToAction("Details", new { id = orderViewModel.OrderId });
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
            HttpResponseMessage response = serviceObj.GetResponse("api/order/" + id.ToString());
            response.EnsureSuccessStatusCode();
            OrderViewModel orderViewModel = response.Content.ReadAsAsync<OrderViewModel>().Result;

            //ViewBag.Title = "All Products";
            return View(orderViewModel);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(OrderViewModel order)
        {
            try
            {
                int id = order.OrderId;
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.DeleteResponse("api/order/" + id.ToString());
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
