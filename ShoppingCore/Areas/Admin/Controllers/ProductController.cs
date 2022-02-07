using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingCore.DataAccess.Repositories.Interfaces;
using ShoppingCore.Model;
using ShoppingCore.Model.ViewModels;
using ShoppingCore.Models;
using ShoppingCore.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCore.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        private readonly IWebHostEnvironment _hostenvoirnment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _UnitOfWork = unitOfWork;
            _hostenvoirnment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var productList = _UnitOfWork.Product.GetAll(includeproperties:"Category,ProductType").ToList();
            return View(productList);
        }
        [HttpGet]
        public IActionResult New()
        {
            ProductVM productVM = new()
            {
                product = new(),
                CategoryList = _UnitOfWork.Catageory.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                ProductTypeList = _UnitOfWork.ProductType.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),

            };
            return View(productVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public IActionResult New(ProductVM obj,IFormFile? file) 
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _hostenvoirnment.WebRootPath;
                if(file!= null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(wwwRootPath, @"Images\Products");
                    var extension = Path.GetExtension(file.FileName);

                    using (var fileStreams = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.product.ImageUrl = @"\Images\Products\" + fileName + extension;
                }
                _UnitOfWork.Product.Add(obj.product);
                _UnitOfWork.Save();
                TempData["Success"] = "...Product has been Created ...";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(Product product)
        {
            if(product !=null)
            { 
            _UnitOfWork.Product.Remove(product);
            _UnitOfWork.Save();
            TempData["Success"] = "Product has been removed Successfully !!";
            }
            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _UnitOfWork.Product.GetAll(includeproperties: "Category,ProductType");
            return Json(new { data = productList});
        }
        #endregion
    }

}
