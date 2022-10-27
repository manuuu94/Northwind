using DAL.Interfaces;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class ShipperDALImpl : IShipperDAL
    {
        //conexion
        NORTHWINDContext context;

        //constructor vacio (sin argumento o parametro)
        public ShipperDALImpl()
        {
            context = new NORTHWINDContext();
        }
        //constructor - ambos son para instanciar un nuevo objeto NORTHWINDContext - se usa cualquiera
        public ShipperDALImpl(NORTHWINDContext northWindContext)
        {
            this.context = northWindContext;
        }

        //CRUD

        //public bool Add(Shipper entity)
        //{
        //    try
        //    {
        //        //la unidad de trabajo, que es generica. tipo category en este caso. cualquier nombre "unidad" para la instancia.
        //        //Dentro del parametro va un context (nombre de la conexion) creado antes. por esto el context requiere un constructor. 
        //        using (UnidadDeTrabajo<Shipper> unidad = new UnidadDeTrabajo<Shipper>(context))
        //        {
        //            //instancia IDALGenerica (i.e: genericDAL.add(entity))
        //            unidad.genericDAL.Add(entity);
        //            //save changes
        //            return unidad.Complete();
        //        }//aqui sucede el dispose por el using a UnidadDeTrabajo con el context (conexion)
        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }
        //}

        public bool Add(Shipper entity)
        {
            try
            {
                string sql = "[dbo].[sp_add_Shipper] @CompanyName, @Phone";
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@CompanyName",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 10,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = entity.CompanyName
                        },
                          new SqlParameter() {
                            ParameterName = "@Phone",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 10,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = entity.Phone
                        }
                };
                context.Database.ExecuteSqlRaw(sql, param);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void AddRange(IEnumerable<Shipper> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Shipper> Find(Expression<Func<Shipper, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Shipper Get(int ShipperId)
        {
            try
            {
                //variable tipo category
                Shipper shipper;
                //using la unidaddetrabajo con entity tipo Category y context
                using (UnidadDeTrabajo<Shipper> unidad = new UnidadDeTrabajo<Shipper>(context))
                {
                    //instancia el metodo de IDAL get a partir de unidaddetrabajo
                    shipper = unidad.genericDAL.Get(ShipperId);
                }
                return shipper;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Shipper> Get()
        {
            try
            {
                IEnumerable<Shipper> shippers;
                using (UnidadDeTrabajo<Shipper> unidad = new UnidadDeTrabajo<Shipper>(context))
                {
                    shippers = unidad.genericDAL.GetAll();
                }
                return shippers.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Shipper> GetAll()
        {
            try
            {
                IEnumerable<Shipper> shippers;
                using (UnidadDeTrabajo<Shipper> unidad = new UnidadDeTrabajo<Shipper>(context))
                {
                    shippers = unidad.genericDAL.GetAll();
                }
                return shippers;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Shipper> GetByName(string Name)
        {
            List<Shipper> lista;
            using (context = new NORTHWINDContext())
            {
                lista = (from c in context.Shippers
                         where c.CompanyName.Contains(Name)
                         select c).ToList();
            }
            return lista;
        }

        public List<Shipper> GetByNameSP(string Name)
        {
            List<sp_GetShippersByName_Result> results;
            string sql = "[dbo].[sp_GetShippersByName] @Name";
            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@Name",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Size = 30,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = Name
                }
            };
            results = context.sp_GetShippersByName_Results.FromSqlRaw(sql, param)
                .ToList();
            List<Shipper> lista = new List<Shipper>();
            foreach (var item in results)
            {
                lista.Add(
                    new Shipper
                    {
                        ShipperId = item.ShipperId,
                        CompanyName = item.CompanyName,
                        Phone = item.Phone
                    });
            }
            return lista;
        }

        public bool Remove(Shipper entity)
        {
            bool result = false;
            try
            {
                using (UnidadDeTrabajo<Shipper> unidad = new UnidadDeTrabajo<Shipper>(context))
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

        public void RemoveRange(IEnumerable<Category> entities)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Shipper> entities)
        {
            throw new NotImplementedException();
        }

        public Category SingleOrDefault(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Shipper SingleOrDefault(Expression<Func<Shipper, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Update(Shipper shipper)
        {
            bool result = false;
            try
            {
                using (UnidadDeTrabajo<Shipper> unidad = new UnidadDeTrabajo<Shipper>(context))
                {
                    unidad.genericDAL.Update(shipper);
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
