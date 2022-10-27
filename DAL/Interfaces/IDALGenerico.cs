using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    //convertir en clase publica, en lugar de internal.
    //parametro de lista, para que reciba cualquier cosa. La clase generica puede ser de cualquier cosa.
    //puede especificar con un where, que entity sea una clase, por ejemplo
    public interface IDALGenerico<TEntity> where TEntity : class
    {
        //un GET
        TEntity Get(int id);
        //un GET ALL a una lista
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        // This method was not in the videos, but I thought it would be useful to add.
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        
        //un ADD o Añadir
        bool Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        //un UPDATE o Actualizar que reciben una clase TEntity, cualquier clase.
        bool Update(TEntity entity);
        //un REMOVE o Eliminar
        bool Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

    }
}
