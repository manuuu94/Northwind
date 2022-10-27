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
    public class OrderController : ControllerBase
    {

        Order Convertir(OrderModel order)
        {
            return new Order
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,  
                OrderDate = order.OrderDate,
                //OrderStatus = order.OrderStatus,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                ShipVia = order.ShipVia,
                Freight = order.Freight,
                ShipName = order.ShipName,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipRegion = order.ShipRegion,
                ShipPostalCode = order.ShipPostalCode,
                ShipCountry = order.ShipCountry

    };
        }

        OrderModel Convertir(Order order)
        {
            return new OrderModel
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                OrderDate = order.OrderDate,
                //OrderStatus = order.OrderStatus,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                ShipVia = order.ShipVia,
                Freight = order.Freight,
                ShipName = order.ShipName,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipRegion = order.ShipRegion,
                ShipPostalCode = order.ShipPostalCode,
                ShipCountry = order.ShipCountry

            };
        }

        IOrderDAL orderDAL;

        public OrderController()
        {
            orderDAL = new OrderDALImpl(new NORTHWINDContext());
        }

        // GET: api/<ProductController>
        [HttpGet]
        public JsonResult Get()
        {
            List<Order> orders = new List<Order>();
            orders = orderDAL.GetAll().ToList();
            List<OrderModel> resultado = new List<OrderModel>();
            foreach (Order order in orders)
            {
                resultado.Add(Convertir(order));
            }
            return new JsonResult(resultado);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            Order order = orderDAL.Get(id);
            return new JsonResult(Convertir(order));
        }

        // POST api/<ProductController>
        [HttpPost]
        public JsonResult Post([FromBody] OrderModel order)
        {
            Order entity = Convertir(order);
            orderDAL.Add(entity);
            return new JsonResult(Convertir(entity));
        }

        // PUT api/<ProductController>/5
        [HttpPut]
        public JsonResult Put([FromBody] OrderModel order)
        {
            Order entity = Convertir(order);
            orderDAL.Update(entity);
            return new JsonResult(Convertir(entity));
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Order order = new Order { OrderId = id };
            orderDAL.Remove(order);

        }
    }
}