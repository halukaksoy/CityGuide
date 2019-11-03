using System;
using System.Collections.Generic;
using System.Text;
using CityGuide.Domain.Interface;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CityGuide.Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private CityGuideDataContext _context;
        public Repository(CityGuideDataContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add<T>(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove<T>(entity);
        }
        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public List<T> GetAll(Func<T, bool> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            query = query.Where(predicate).AsQueryable();
            return query.ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
