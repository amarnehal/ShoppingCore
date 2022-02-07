using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingCore.DataAccess.Repositories.Interfaces;
using ShoppingCore.Model;
using ShoppingCore.Models;
using ShoppingCore.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShoppingCore.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _UnitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _UnitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
           var allProducts = _UnitOfWork.Product.GetAll(includeproperties:"Category,ProductType").ToList();
            return View(allProducts);
        }
        [HttpGet]
        public IActionResult Details(int productid)
        {
            ShoppingCart cart = new ShoppingCart()
            {
                ProductId = productid,
                Count = 1,
                Product = _UnitOfWork.Product.GetFirstOrDefault(x => x.Id == productid, includeproperties: "Category,ProductType"),
            };
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            //Retreiveing user Id
            var claimsIdentity =(ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            // adding items to existing cart
            ShoppingCart cartDb = _UnitOfWork.ShoppingCart.GetFirstOrDefault(x => x.ApplicationUserId == claim.Value && x.ProductId == shoppingCart.ProductId);

            if(cartDb == null)
            {
                _UnitOfWork.ShoppingCart.Add(shoppingCart);
                _UnitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, _UnitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);
            }
             else
            {
                _UnitOfWork.ShoppingCart.incrementCount(cartDb, shoppingCart.Count);
            }
            
            _UnitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
