using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBookShop.Data;
using Bullky.Models;
using BullkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDBContext context;
        public ProductRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }
        public void Update(Product product)
        {
            var ProductfromDB =context.products
                .FirstOrDefault(product => product.Id == product.Id);
            if (ProductfromDB != null)
            {
                ProductfromDB.Title = product.Title;
                ProductfromDB.ISBN = product.Title;
                ProductfromDB.Price = product.Price;
                ProductfromDB.Price50 = product.Price50;
                ProductfromDB.Price100 = product.Price100;
                ProductfromDB.ListPrice = product.ListPrice;
                ProductfromDB.Author = product.Author;
                ProductfromDB.Description = product.Description;
                ProductfromDB.CategoryID = product.CategoryID;
                if (product.ImageURL != null)
                {
                    ProductfromDB.ImageURL = product.ImageURL;
                }
            }
        }
    }
}
