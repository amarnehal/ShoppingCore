using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCore.DataAccess.Repositories.Interfaces;
using ShoppingCore.Model;
using ShoppingCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductTypeController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public ProductTypeController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var productTypeList = _UnitOfWork.ProductType.GetAll().ToList();
            return View(productTypeList);
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        public IActionResult New(ProductType productType)
        {
            if(ModelState.IsValid)
            { 
            var newProductType = new ProductType()
            {
                Name = productType.Name,
            };
            _UnitOfWork.ProductType.Add(newProductType);
            _UnitOfWork.Save();
            TempData["Success"] = "Product Type has been Added ...";
            return RedirectToAction("Index");
            }
            return View();
        }
    }
}
