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
    public class SupplierDALImpl : ISupplierDAL
    {
        NORTHWINDContext context;

        //constructor vacio (sin argumento o parametro)
        public SupplierDALImpl()
        {
            context = new NORTHWINDContext();
        }
        //constructor - ambos son para instanciar un nuevo objeto NORTHWINDContext - se usa cualquiera
        public SupplierDALImpl(NORTHWINDContext northWindContext)
        {
            this.context = northWindContext;
        }

        public bool Add(Supplier entity)
        {
            try
            {
                //la unidad de trabajo, que es generica. tipo category en este caso. cualquier nombre "unidad" para la instancia.
                //Dentro del parametro va un context (nombre de la conexion) creado antes. por esto el context requiere un constructor. 
                using (UnidadDeTrabajo<Supplier> unidad = new UnidadDeTrabajo<Supplier>(context))
                {
                    //instancia IDALGenerica (i.e: genericDAL.add(entity))
                    unidad.genericDAL.Add(entity);
                    //save changes
                    return unidad.Complete();
                }//aqui sucede el dispose por el using a UnidadDeTrabajo con el context (conexion)
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void AddRange(IEnumerable<Supplier> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Supplier> Find(Expression<Func<Supplier, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Supplier Get(int SupplierId)
        {
            try
            {
                //variable tipo category
                Supplier supplier;
                //using la unidaddetrabajo con entity tipo Category y context
                using (UnidadDeTrabajo<Supplier> unidad = new UnidadDeTrabajo<Supplier>(context))
                {
                    //instancia el metodo de IDAL get a partir de unidaddetrabajo
                    supplier = unidad.genericDAL.Get(SupplierId);
                }
                return supplier;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Supplier> Get()
        {
            try
            {
                IEnumerable<Supplier> suppliers;
                using (UnidadDeTrabajo<Supplier> unidad = new UnidadDeTrabajo<Supplier>(context))
                {
                    suppliers = unidad.genericDAL.GetAll();
                }
                return suppliers.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Supplier> GetAll()
        {
            try
            {
                IEnumerable<Supplier> suppliers;
                using (UnidadDeTrabajo<Supplier> unidad = new UnidadDeTrabajo<Supplier>(context))
                {
                    suppliers = unidad.genericDAL.GetAll();
                }
                return suppliers;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Supplier> GetByName(string CompanyName)
        {
            List<Supplier> lista;

            using (context = new NORTHWINDContext())
            {
                lista = (from c in context.Suppliers
                         where c.CompanyName.Contains(CompanyName)
                         select c).ToList();
            }
            return lista;

        }



        public bool Remove(Supplier entity)
        {
            bool result = false;
            try
            {
                using (UnidadDeTrabajo<Supplier> unidad = new UnidadDeTrabajo<Supplier>(context))
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

        public void RemoveRange(IEnumerable<Supplier> entities)
        {
            throw new NotImplementedException();
        }

        public Category SingleOrDefault(Expression<Func<Supplier, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(Supplier supplier)
        {
            bool result = false;

            try
            {
                using (UnidadDeTrabajo<Supplier> unidad = new UnidadDeTrabajo<Supplier>(context))
                {
                    unidad.genericDAL.Update(supplier);
                    result = unidad.Complete();
                }

            }
            catch (Exception)
            {

                return false;
            }

            return result;
        }

        Supplier IDALGenerico<Supplier>.SingleOrDefault(Expression<Func<Supplier, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}