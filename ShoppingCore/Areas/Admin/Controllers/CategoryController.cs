using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCore.DataAccess.Repositories.Implementations;
using ShoppingCore.DataAccess.Repositories.Interfaces;
using ShoppingCore.Models;
using ShoppingCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var getCategoryList = _UnitOfWork.Catageory.GetAll();
            return View(getCategoryList);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Category model)
        {
            if(ModelState.IsValid)
            {
                var newCategory = new Category()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    DateCreated = DateTime.Now

                };
                _UnitOfWork.Catageory.Add(newCategory);
                _UnitOfWork.Save();
                TempData["Success"] = "New Category has been created !!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id ==null || id == 0)
            {
                return NotFound();
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                var categoryToEdit = new Category()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                    DisplayOrder = category.DisplayOrder,
                    DateCreated = DateTime.Now

                };
                _UnitOfWork.Catageory.Update(categoryToEdit);
                _UnitOfWork.Save();
                TempData["Success"] = "Category has been Updated Successfully !!";
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public IActionResult Delete(Category category)
        {
            if(category != null)
            { 
                _UnitOfWork.Catageory.Remove(category);
                _UnitOfWork.Save();
                TempData["Success"] = "Category has been removed Successfully !!";
            }
            return RedirectToAction("Index");
            
        }
    }
}
