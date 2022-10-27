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
    public class SupplierController : ControllerBase
    {

        private ISupplierDAL supplierDAL;
        SupplierModel Convertir(Supplier supplier)
        {
            return new SupplierModel
            {
                SupplierId = supplier.SupplierId,
                CompanyName = supplier.CompanyName
            };
        }

        List<SupplierModel> Convertir(List<Supplier> suppliers)
        {
            List<SupplierModel> lista = new List<SupplierModel>();

            foreach (Supplier supplier in suppliers)
            {
                lista.Add(Convertir(supplier));
            }


            return lista;
        }


        Supplier Convertir(SupplierModel supplier)
        {
            return new Supplier
            {
                SupplierId = supplier.SupplierId,
                CompanyName = supplier.CompanyName
            };
        }
        //constructor
        public SupplierController()
        {
            supplierDAL = new SupplierDALImpl(new Entities.NORTHWINDContext());
        }

        #region Consultar
        // GET: api/<CategoryController>
        //[HttpGet]
        //public JsonResult Get()
        //{
        //    IEnumerable<Supplier> suppliers;
        //    suppliers = supplierDAL.GetAll();
        //    return new JsonResult(suppliers);
        //}

        //// GET api/<CategoryController>/5
        //[HttpGet("{id}", Name = "Get3")]
        //public JsonResult Get(int id)
        //{
        //    Supplier supplier;
        //    supplier = supplierDAL.Get(id);
        //    return new JsonResult(supplier);
        //}
        [HttpGet]
        public JsonResult Get()
        {
            List<Supplier> suppliers;
            suppliers = supplierDAL.GetAll().ToList();
            return new JsonResult(Convertir(suppliers));
        }

        // GET api/<SupplierController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            Supplier supplier;
            supplier = supplierDAL.Get(id);
            return new JsonResult(Convertir(supplier));
        }
        #endregion

        #region Agregar
        // POST api/<CategoryController>
        [HttpPost]
        public JsonResult Post([FromBody] Supplier supplier)
        {
            try
            {
                supplierDAL.Add(supplier);
                return new JsonResult(supplier);
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
        public JsonResult Put([FromBody] Supplier supplier)
        {
            try
            {
                supplierDAL.Update(supplier);
                return new JsonResult(supplier);
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
        public JsonResult Delete(int id)
        {
            try
            {
                Supplier supplier = new Supplier { SupplierId = id };
                //category = categoryDAL.Get(id);
                supplierDAL.Remove(supplier);
                return new JsonResult(supplier);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
