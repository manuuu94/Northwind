using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class ShipperController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                ServiceRepository Repository = new ServiceRepository();
                HttpResponseMessage responseMessage = Repository.GetResponse("api/shipper");
                responseMessage.EnsureSuccessStatusCode();
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                List<ShipperViewModel> shippers = JsonConvert.DeserializeObject<List<ShipperViewModel>>(content);
                return View(shippers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/shipper/" + id.ToString());
                response.EnsureSuccessStatusCode();
                Models.ShipperViewModel shipperViewModel = response.Content.ReadAsAsync<Models.ShipperViewModel>().Result;
                return View(shipperViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ShipperViewModel shipper)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PostResponse("api/shipper/",shipper);
                response.EnsureSuccessStatusCode();
                ShipperViewModel shipperViewModel = response.Content.ReadAsAsync<ShipperViewModel>().Result;
                return View(shipperViewModel);

                return RedirectToAction("Details", new { id = shipperViewModel.ShipperId });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/shipper/" + id.ToString());
                response.EnsureSuccessStatusCode();
                Models.ShipperViewModel shipperViewModel = response.Content.ReadAsAsync<Models.ShipperViewModel>().Result;
                return View(shipperViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Edit(ShipperViewModel shipper)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PostResponse("api/shipper/", shipper);
                response.EnsureSuccessStatusCode();
                ShipperViewModel shipperViewModel = response.Content.ReadAsAsync<ShipperViewModel>().Result;
                //return View(categoryViewModel);

                return RedirectToAction("Index", new { id = shipperViewModel.ShipperId });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
