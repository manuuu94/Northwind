using FrontEnd.Helpers;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                //instancia el repository en Helpers
                ServiceRepository Repository = new ServiceRepository();
                //devuelve un GET del repository, usando Category. El repository contiene un CRUD para utilizar generico.
                HttpResponseMessage responseMessage = Repository.GetResponse("api/category");
                //si no da 200 (OK), se cae. Necesita Try y Catch
                responseMessage.EnsureSuccessStatusCode();
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                //son asyncronos porque podemos enviar un comando al api y seguir trabajando en la aplicacion mientras trabaja el api
                //convertimos el JSON (lista de categories) que trae el API y lo guardamos en el ViewModel
                List<CategoryViewModel> categories = JsonConvert.DeserializeObject<List<CategoryViewModel>>(content); //lista

                return View(categories);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //
        // GET: CategoryController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                //concatenarle el ID para el argumento 
                HttpResponseMessage response = serviceObj.GetResponse("api/category/" + id.ToString());
                response.EnsureSuccessStatusCode();
                //devuelve solo un producto tipo catviewmodel
                //CategoryViewModel categoryViewModel = response.Content.ReadAsAsync<Models.CategoryViewModel>().Result;
                Models.CategoryViewModel categoryViewModel = response.Content.ReadAsAsync<Models.CategoryViewModel>().Result; 
                return View(categoryViewModel);
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
        public ActionResult Create(CategoryViewModel category)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PostResponse("api/category/",category);
                response.EnsureSuccessStatusCode();
                CategoryViewModel categoryViewModel = response.Content.ReadAsAsync<CategoryViewModel>().Result;
                return View(categoryViewModel);

                return RedirectToAction("Details",new {id = categoryViewModel.CategoryId});  
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
                HttpResponseMessage response = serviceObj.GetResponse("api/category/" + id.ToString());
                response.EnsureSuccessStatusCode();
                Models.CategoryViewModel categoryViewModel = response.Content.ReadAsAsync<Models.CategoryViewModel>().Result;
                return View(categoryViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel category)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.PutResponse("api/category/", category);
                response.EnsureSuccessStatusCode();
                CategoryViewModel categoryViewModel = response.Content.ReadAsAsync<CategoryViewModel>().Result;
                //return View(categoryViewModel);

                return RedirectToAction("Details", new { id = category.CategoryId });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/category/" + id.ToString());
                response.EnsureSuccessStatusCode();
                Models.CategoryViewModel categoryViewModel = response.Content.ReadAsAsync<Models.CategoryViewModel>().Result;
                return View(categoryViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[HttpDelete]
        public ActionResult Delete(CategoryViewModel category)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.DeleteResponse("api/category/"+ category.CategoryId.ToString());
                response.EnsureSuccessStatusCode();
                bool Eliminado = response.Content.ReadAsAsync<bool>().Result;
                //return View(categoryViewModel);
                if (Eliminado)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
