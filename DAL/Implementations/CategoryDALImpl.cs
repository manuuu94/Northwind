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

//namespace BackEnd.DAL
namespace DAL.Implementations
{
    //cambiar a public class : implementar la interfaz ICategoryDAL
    //comentar y copiar de NorthWind que ya tiene el código programado y constructores
    public class CategoryDALImpl : ICategoryDAL

    {
        //conexion
        NORTHWINDContext context;

        //constructor vacio (sin argumento o parametro)
        public CategoryDALImpl()
        {
            context = new NORTHWINDContext();
        }
        //constructor - ambos son para instanciar un nuevo objeto NORTHWINDContext - se usa cualquiera
        public CategoryDALImpl(NORTHWINDContext northWindContext)
        {
            this.context = northWindContext;
        }

        //crud

        //metodo para añadir una nueva Category por objeto. Cada entidad tendria una.

        //public bool Add(Category entity)
        //    {
        //        try
        //        {
        //        //la unidad de trabajo, que es generica. tipo category en este caso. cualquier nombre "unidad" para la instancia.
        //        //Dentro del parametro va un context (nombre de la conexion) creado antes. por esto el context requiere un constructor. 
        //            using (UnidadDeTrabajo<Category> unidad = new UnidadDeTrabajo<Category>(context))
        //            {
        //            //instancia IDALGenerica (i.e: genericDAL.add(entity))
        //                unidad.genericDAL.Add(entity);
        //            //save changes
        //                return unidad.Complete();
        //            }//aqui sucede el dispose por el using a UnidadDeTrabajo con el context (conexion)

        //        }
        //        catch (Exception)
        //        {

        //            return false;
        //        }
        //    }

        //metodo para añadir una nueva Category por SP. Cada entidad tendria una.
        public bool Add(Category entity)
        {
            try
            {
                //no requiere ser mapeado de DBContext porque no regresa nada. Solo True o False. 
                //ejecuta el SQL directamente, sin retornar ExecuteSqlRaw
                string sql = "[dbo].[sp_add_Category] @CategoryName, @Description";
                var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@CategoryName",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 10,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = entity.CategoryName
                        },
                          new SqlParameter() {
                            ParameterName = "@Description",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 10,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = entity.Description
                        }
                          //no pone picture porque no importa que vaya vacio, y el ID es automático.
                          //para editar la DB se usar ExecuteSqlRaw. Para Consultar FromSqlRaw.
                };
                context.Database.ExecuteSqlRaw(sql, param);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void AddRange(IEnumerable<Category> entities)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Category> Find(Expression<Func<Category, bool>> predicate)
            {
                throw new NotImplementedException();
            }

            public Category Get(int CategoryId)
            {
                try
                {
                //variable tipo category
                    Category category;
                //using la unidaddetrabajo con entity tipo Category y context
                    using (UnidadDeTrabajo<Category> unidad = new UnidadDeTrabajo<Category>(context))
                    {
                    //instancia el metodo de IDAL get a partir de unidaddetrabajo
                        category = unidad.genericDAL.Get(CategoryId);
                    }
                    return category;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public List<Category> Get()
            {
                try
                {
                    IEnumerable<Category> categories;
                    using (UnidadDeTrabajo<Category> unidad = new UnidadDeTrabajo<Category>(context))
                    {
                        categories = unidad.genericDAL.GetAll();
                    }
                    return categories.ToList();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            public IEnumerable<Category> GetAll()
            {
                try
                {
                    IEnumerable<Category> categories;
                    using (UnidadDeTrabajo<Category> unidad = new UnidadDeTrabajo<Category>(context))
                    {
                        categories = unidad.genericDAL.GetAll();
                    }
                    return categories;
                }
                catch (Exception)
                {

                    throw;
                }
            }

        //no tiene controller. es lo mismo al de abajo pero en linq
            public List<Category> GetByName(string Name)
            {
                List<Category> lista;
                using (context = new NORTHWINDContext())
                {
                    lista = (from c in context.Categories
                             where c.CategoryName.Contains(Name)
                             select c).ToList();
                }
                return lista;
            }

        //StoredProcedureGetByName
        public List<Category> GetByNameSP(string Name)
        {
            //se crea una lista
            List<sp_GetCategoriesByName_Result> results;
            //se crea un string con el nombre del sp + parámetro
            string sql = "[dbo].[sp_GetCategoriesByName] @Name";
            //parametros con SqlParameter que es un arreglo
            var param = new SqlParameter[]
            {
//1er parametro - si hubiera otro solo se copia despues de , y se cambia los nombres
                new SqlParameter()
                {
                    //debe coincidir con el sp @
                    ParameterName = "@Name",
                    //tipo de variable en el sql
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    //tamaño de la variable
                    Size = 10,
                    //no es obligatorio
                    Direction = System.Data.ParameterDirection.Input,
                    //nombre del argumento que recibe
                    Value = Name
                }
            };
            //devuelve un iquerable tipo sp. lo pasamos a lista.
            //resultados del SP en Sql y lo pasamos a una lista de Category
            results = context.sp_GetCategoriesByName_Results.FromSqlRaw(sql,param).ToListAsync().Result;
            //crea lista tipo Category y la llena
            List<Category> lista = new List<Category>();
            foreach (var item in results)
            {
                lista.Add(
                    //se instancia el objeto y sus atributos
                    new Category
                    {
                        CategoryId = item.CategoryId,
                        CategoryName = item.CategoryName,
                        Description = item.Description,
                        Picture = item.Picture
                    });
            }
            return lista;
        }

        public bool Remove(Category entity)
            {
                bool result = false;
                try
                {
                    using (UnidadDeTrabajo<Category> unidad = new UnidadDeTrabajo<Category>(context))
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

            public Category SingleOrDefault(Expression<Func<Category, bool>> predicate)
            {
                throw new NotImplementedException();
            }

            public bool Update(Category category)
            {
                bool result = false;

                try
                {
                    using (UnidadDeTrabajo<Category> unidad = new UnidadDeTrabajo<Category>(context))
                    {
                        unidad.genericDAL.Update(category);
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