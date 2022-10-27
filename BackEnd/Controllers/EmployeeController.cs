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
    public class EmployeeController : ControllerBase
    {
        private IEmployeeDAL employeeDAL;

        EmployeeModel Convertir(Employee employee)
        {
            return new EmployeeModel
            {
                EmployeeId = employee.EmployeeId,
                LastName = employee.LastName,
                FirstName = employee.FirstName
            };
        }

        List<EmployeeModel> Convertir(List<Employee> employees)
        {
            List<EmployeeModel> lista = new List<EmployeeModel>();
            foreach (Employee employee in employees)
            {
                lista.Add(Convertir(employee));
            }
            return lista;
        }

        Employee Convertir(EmployeeModel employee)
        {
            return new Employee
            {
                EmployeeId = employee.EmployeeId,
                LastName = employee.LastName,
                FirstName = employee.FirstName
            };
        }
        //constructor
        public EmployeeController()
        {
            employeeDAL = new EmployeeDALImpl(new Entities.NORTHWINDContext());
        }


        #region Consultar
        [HttpGet]
        public JsonResult Get()
        {
            List<Employee> employees;
            employees = employeeDAL.GetAll().ToList();
            return new JsonResult(Convertir(employees));
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            Employee employee;
            employee = employeeDAL.Get(id);
            return new JsonResult(Convertir(employee));
        }
        #endregion

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
