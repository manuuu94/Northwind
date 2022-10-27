using BackEnd.Models;
using DAL.Implementations;
using DAL.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        //instancia la interfaz de la entidad
        private ICategoryDAL categoryDAL;
       
        //método para convertir
        private CategoryModel Convertir(Category category)
        {
            return new CategoryModel
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    Description = category.Description
                };           
        }

        //constructor
        public CategoryController()
        {
            categoryDAL = new CategoryDALImpl(new Entities.NORTHWINDContext());
        }

        // GET: api/<CategoryController>
        /*       [HttpGet]
               public IEnumerable<string> Get()
               {
                   return new string[] { "value1", "value2" };
               } 
        */

        #region Consultar
        // GET: api/<CategoryController>
        [HttpGet]
        public JsonResult Get()
        {


            IEnumerable<Category> categories;
            categories = categoryDAL.GetAll();

            //para mostrar el nuevomodel, en lugar del ENTITY
            List<CategoryModel> result = new List<CategoryModel>();
            foreach( Category category in categories)
            {
                result.Add(Convertir(category)
                //    new CategoryModel
                //{
                //    CategoryId = category.CategoryId,
                //    CategoryName = category.CategoryName,
                //    Description = category.Description
                //}
                    );
            }


            return new JsonResult(result);
        }

        [Route("GetByNameSP")]
        [HttpGet]
        public JsonResult Get(string Name)
        {
            IEnumerable<Category> categories;
            categories = categoryDAL.GetByNameSP(Name);

            List<CategoryModel> result = new List<CategoryModel>();
            foreach (Category category in categories)
            {
                result.Add(Convertir(category)
                    );
            }
                return new JsonResult(result);
        }


        // GET api/<CategoryController>/5
        [HttpGet("{id}", Name = "Get")]
        public JsonResult Get(int id)
        {

            Category category;
            category = categoryDAL.Get(id);
            //para utilizar los backend Models, en lugar de las entidades, para mostrar en los views del frontend. 
            //CategoryModel model = new CategoryModel
            //{
            //    CategoryId = category.CategoryId,
            //    CategoryName = category.CategoryName,
            //    Description = category.Description
            //};
            //en lugar de category, devuelvo model.
            return new JsonResult(Convertir(category)); //category / model
        }
        #endregion

        #region Agregar
        // POST api/<CategoryController>
        //funciona igual para el SP que el original generico.
        [HttpPost]
        public JsonResult Post([FromBody] Category category)
        {
            try
            {
                categoryDAL.Add(category); //está agregando por medio del SP en Impl

                return new JsonResult(Convertir(category));
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Actualizar

        // PUT api/<CategoryController>/5
        [HttpPut]
        public JsonResult Put([FromBody] Category category)
        {
            try
            {
                categoryDAL.Update(category);

                return new JsonResult(Convertir(category));

            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion


        #region Eliminar
        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            try
            {

                Category category = new Category { CategoryId = id };
                //category = categoryDAL.Get(id);
                
                categoryDAL.Remove(category);
                return true;


            }
            catch (Exception)
            {

                throw;          
            }


        }
        #endregion
    }
}