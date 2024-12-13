using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBookShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category {  get; private set; }

        public IProductRepository Product {get; private set; }

        AppDBContext context;
        public UnitOfWork(AppDBContext context) 
        {
            this.context = context;
            Category = new CategoryRepository(context);
            Product =new ProductRepository(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
