using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AskStorage.AccesoDatos.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        //Obtener valores byId *Select*
        T Get(int id);
        //Obtener valores *Select*
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
        );

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
        );

        //Insertar
        void Add(T entity);

        //Eliminar
        void Remove(int id);

        void Remove(T entity);
    }
}
