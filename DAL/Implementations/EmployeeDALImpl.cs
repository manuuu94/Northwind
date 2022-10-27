using DAL.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class EmployeeDALImpl : IEmployeeDAL
    {
        private UnidadDeTrabajo<Employee> unidad;
        NORTHWINDContext context;
        public EmployeeDALImpl(NORTHWINDContext context)
        {
            this.context = context;
        }


        public bool Add(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Employee> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> Find(Expression<Func<Employee, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Employee Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> GetAll()
        {
            IEnumerable<Employee> entities;
            using (unidad = new UnidadDeTrabajo<Employee>(context))
            {
                entities = unidad.genericDAL.GetAll();
            }
            return entities;
        }

        public bool Remove(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Employee> entities)
        {
            throw new NotImplementedException();
        }

        public Employee SingleOrDefault(Expression<Func<Employee, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(Employee entity)
        {
            throw new NotImplementedException();
        }
    }
}
