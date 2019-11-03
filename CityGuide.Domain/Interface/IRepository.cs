using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace CityGuide.Domain.Interface
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        void Add(T entity);
        void Delete(T entity);
        bool SaveAll();

        T Get(int id);
        List<T> GetAll();
        List<T> GetAll(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes);
    }
}
