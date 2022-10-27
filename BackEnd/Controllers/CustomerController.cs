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
    public class CustomerController : ControllerBase
    {
        private ICustomerDAL customerDAL;

        CustomerModel Convertir(Customer customer)
        {
            return new CustomerModel
            {
                CustomerId = customer.CustomerId,
                CompanyName = customer.CompanyName
            };
        }

        List<CustomerModel> Convertir(List<Customer> customers)
        {
            List<CustomerModel> lista = new List<CustomerModel>();
            foreach (Customer customer in customers)
            {
                lista.Add(Convertir(customer));
            }
            return lista;
        }

        Customer Convertir(CustomerModel customer)
        {
            return new Customer
            {
                CustomerId = customer.CustomerId,
                CompanyName = customer.CompanyName
            };
        }

        //constructor
        public CustomerController()
        {
            customerDAL = new CustomerDALImpl(new Entities.NORTHWINDContext());
        }


        #region Consultar
        [HttpGet]
        public JsonResult Get()
        {
            List<Customer> customers;
            customers = customerDAL.GetAll().ToList();
            return new JsonResult(Convertir(customers));
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            Customer customer;
            customer = customerDAL.Get(id);
            return new JsonResult(Convertir(customer));
        }
        #endregion

        // POST api/<CutomerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CutomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CutomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
