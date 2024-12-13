using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBookShop.Data;
using Bullky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDBContext context;
        public CategoryRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }

        public void Update(Category category)
        {
           context.Categories.Update(category);
        }
    }
}
