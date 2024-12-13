using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBookShop.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDBContext context;
        private readonly DbSet<T> dbSet;
        public Repository(AppDBContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
            //context.category = dbset
            //context.products.Include(u => u.Category).Include(u => u.CategoryID);
        }
        public void Add(T entity)
        {
           dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter , string? IncludeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (!(string.IsNullOrEmpty(IncludeProperties)))
            {
                foreach (var includeprop in IncludeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }
            return query.FirstOrDefault();
        }
        //Category,Cover Type
        public IEnumerable<T> GetAll(string? IncludeProperties =null)
        {
            IQueryable<T> query = dbSet;
            if (!(string.IsNullOrEmpty(IncludeProperties)))
            {
                foreach (var includeprop in IncludeProperties
                    .Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeprop);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
