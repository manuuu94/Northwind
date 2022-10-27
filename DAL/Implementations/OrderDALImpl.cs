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
    public class OrderDALImpl : IOrderDAL
    {
        private UnidadDeTrabajo<Order> unidad;
        NORTHWINDContext context;
        public OrderDALImpl(NORTHWINDContext context)
        {
            this.context = context;
        }




        public bool Add(Order entity)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<Order>(context))
                {
                    unidad.genericDAL.Add(entity);
                    unidad.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void AddRange(IEnumerable<Order> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Order Get(int id)
        {
            Order order = null;
            using (unidad = new UnidadDeTrabajo<Order>(context))
            {
                order = unidad.genericDAL.Get(id);
            }
            return order;
        }

        public IEnumerable<Order> GetAll()
        {
            IEnumerable<Order> orders = null;
            using (unidad = new UnidadDeTrabajo<Order>(context))
            {
                orders = unidad.genericDAL.GetAll();
            }
            return orders;
        }

        public bool Remove(Order entity)
        {
            bool result = false;
            try
            {
                using (unidad = new UnidadDeTrabajo<Order>(context))
                {
                    unidad.genericDAL.Remove(entity);
                    result = unidad.Complete();
                }

            }
            catch (Exception)
            {

                result = false;
            }

            return result;
        }

        public void RemoveRange(IEnumerable<Order> entities)
        {
            throw new NotImplementedException();
        }

        public Order SingleOrDefault(Expression<Func<Order, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(Order entity)
        {
            bool result = false;
            try
            {
                using (UnidadDeTrabajo<Order> unidad = new UnidadDeTrabajo<Order>(context))
                {
                    unidad.genericDAL.Update(entity);
                    result = unidad.Complete();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return result;
        }
    }
}
