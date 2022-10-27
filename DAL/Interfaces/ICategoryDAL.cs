using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    //nomenclatura correcta: I de interfaz, Category el nombre de la entidad, DAL la capa
    //hacerlo una clase publica y que herede los metodos de IDALGENERICO. En parametro se especifica cual entidad se hace referencia
    public interface ICategoryDAL : IDALGenerico<Category>
    {
        //en la implementacion añadimos los metodos que van en la IMPL. 
        //se deben implementar en IMPL
        //método para el SP getbyname:
        List<Category> GetByName(string Name); //no tiene controller

        //StoredProcedure getByName
        List<Category> GetByNameSP(string Name);

    }
}
