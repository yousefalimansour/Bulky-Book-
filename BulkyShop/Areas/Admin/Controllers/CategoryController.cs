using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBookShop.Data;
using Bullky.Models;
using Bullky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace BulkyBookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }



        public IActionResult Index()
        {
            List<Category> categories = unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name can't by the same as Display Order");
            }
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Add(category);
                unitOfWork.Save();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            Category categoryfromDB =
            unitOfWork.Category.Get(x => x.Id == id);

            if (categoryfromDB == null) return NotFound();

            return View(categoryfromDB);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {

            if (ModelState.IsValid)
            {
                unitOfWork.Category.Update(category);
                unitOfWork.Save();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View("Index", category);
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            Category categoryfromDB =
                unitOfWork.Category.Get(x => x.Id == id);

            if (categoryfromDB == null) return NotFound();

            return View(categoryfromDB);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult SaveDelete(int? id)
        {
            Category? category = unitOfWork.Category.Get(x => x.Id == id);
            if (category == null) return NotFound();
            unitOfWork.Category.Remove(category);
            unitOfWork.Save();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }

    }
}
