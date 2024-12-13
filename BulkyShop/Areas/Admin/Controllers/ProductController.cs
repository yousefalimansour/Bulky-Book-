using BulkyBook.DataAccess.Repository.IRepository;
using Bullky.Models;
using BullkyBook.Models;
using BullkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Collections.Generic;

namespace BulkyBookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> products = unitOfWork.Product.GetAll(IncludeProperties: "Category").ToList();            
            return View(products);
        }
        public IActionResult Upsert(int? id) //Update+Insert
        {
            ProductViewModel productVM = new()
            {
                ItemList = unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
               
            };
            if (id == null || id == 0)
            {
                //Creat
                return View(productVM);
            }
            else
            {
                //Update
                productVM.Product = unitOfWork.Product.Get(i=>i.Id==id);
                return View(productVM);
            }
           
        }
        [HttpPost]
        public IActionResult Upsert(ProductViewModel _product, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath =webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString()
                        + Path.GetExtension(file.FileName);
                    string productpath = Path.Combine(wwwRootPath, @"Images\product");

                    if (!(String.IsNullOrEmpty(_product.Product.ImageURL)))
                    {
                        //delete old image 
                        var oldpath =
                            Path.Combine(wwwRootPath, _product.Product.ImageURL.TrimStart('\\'));
                        if(System.IO.File.Exists(oldpath))
                        {
                            System.IO.File.Delete(oldpath);
                        }
                    }


                    using (var filestream = new FileStream(Path.Combine(productpath, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }

                    _product.Product.ImageURL = @"\Images\Product\" + filename;
                        

                }
                if(_product.Product.Id == 0)
                {
                    unitOfWork.Product.Add(_product.Product);
                    TempData["success"] = "Product Created Successfully";
                }
                else
                {
                    unitOfWork.Product.Update(_product.Product);
                    TempData["success"] = "Product Update Successfully";
                }
               
                unitOfWork.Save();
                
                return RedirectToAction("Index");
            }
            else
            {
                _product.ItemList = unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
               
                return View(_product);
            }
           
        }
        /*public IActionResult Delete(int? id)
        //{
        //    if (id is null || id == 0)
        //    {
        //        return NotFound();
        //    }

        //    Product ProductfromDB =
        //    unitOfWork.Product.Get(x => x.Id == id);

        //    if (ProductfromDB == null) return NotFound();

        //    return View(ProductfromDB);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult SaveDelete(int? id)
        //{
        //    Product? Product =
        //    unitOfWork.Product.Get(x => x.Id == id);
        //    if ( Product == null) return NotFound();
        //    unitOfWork.Product.Remove(Product);
        //    unitOfWork.Save();
        //    TempData["success"] = "Product Deleted Successfully";
        //    return RedirectToAction("Index");         
        }
        */

        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = unitOfWork.Product.GetAll(IncludeProperties: "Category").ToList();
            return Json(new { data = products });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            Product ProductfromDB =
            unitOfWork.Product.Get(x => x.Id == id);
            if (ProductfromDB == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }

            var oldpath =
                Path.Combine(webHostEnvironment.WebRootPath,
                ProductfromDB.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(oldpath))
            {
                System.IO.File.Delete(oldpath);
            }
            unitOfWork.Product.Remove(ProductfromDB);
            unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully" });
        }
        #endregion
    }
}
