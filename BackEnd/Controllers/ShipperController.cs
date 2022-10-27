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
    public class ShipperController : ControllerBase
    {
        private IShipperDAL shipperDAL;
        private ShipperModel Convertir(Shipper shipper)
        {
            return new ShipperModel
            {
                ShipperId = shipper.ShipperId,
                CompanyName = shipper.CompanyName
            };
        }
        public ShipperController()
        {
            shipperDAL = new ShipperDALImpl(new Entities.NORTHWINDContext());
        }

        // GET: api/<ShipperController>
        /*       [HttpGet]
               public IEnumerable<string> Get()
               {
                   return new string[] { "value1", "value2" };
               }
        */


        [HttpGet]
        //[Route("ObtenerShippers")]
        public JsonResult Get()
        {
            IEnumerable<Shipper> shippers;
            shippers = shipperDAL.GetAll();
            //categories encapzuladas

            //para mostrar el nuevomodel, en lugar del ENTITY
            List<ShipperModel> result = new List<ShipperModel>();
            foreach (Shipper shipper in shippers)
            {
                result.Add(Convertir(shipper));
            }

            return new JsonResult(result); 
        }

        [Route("GetByNameSP")]
        [HttpGet]
        public JsonResult Get(string Name)
        {
            IEnumerable<Shipper> shippers;
            shippers = shipperDAL.GetByNameSP(Name);

            List<ShipperModel> result = new List<ShipperModel>();
            foreach (Shipper shipper in shippers)
            {
                result.Add(Convertir(shipper));
            }

            return new JsonResult(result);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}", Name = "Get2")]
        public JsonResult Get(int id)
        {

            Shipper shipper;
            shipper = shipperDAL.Get(id);

            //ShipperModel model = new ShipperModel
            //{
            //    ShipperId = shipper.ShipperId,
            //    CompanyName = shipper.CompanyName,
            //};

            return new JsonResult(Convertir(shipper)); //shipper
        }


        #region Agregar
        // POST api/<CategoryController>
        [HttpPost]
        public JsonResult Post([FromBody] Shipper shipper)
        {

            try
            {
                shipperDAL.Add(shipper);
                return new JsonResult(Convertir(shipper));
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
        public JsonResult Put([FromBody] Shipper shipper)
        {
            try
            {
                shipperDAL.Update(shipper);
                return new JsonResult(Convertir(shipper));

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

                Shipper shipper= new Shipper{ ShipperId= id };
                //category = categoryDAL.Get(id);

                shipperDAL.Remove(shipper);
                return new JsonResult(Convertir(shipper));


            }
            catch (Exception)
            {

                throw;
            }


        }
        #endregion
    }
}