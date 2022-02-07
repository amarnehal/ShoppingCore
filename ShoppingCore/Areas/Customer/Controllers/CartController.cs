using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCore.DataAccess.Repositories.Interfaces;
using ShoppingCore.Model;
using ShoppingCore.Model.ViewModels;
using ShoppingCore.Utilities;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShoppingCore.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
        public int OrderTotal { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //Retreiveing user Id
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartViewModel = new ShoppingCartViewModel()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == claim.Value, includeproperties: "Product"),
                 OrderHeader = new OrderHeader()
            };
            //to get total 
            foreach(var cart in ShoppingCartViewModel.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price);
                ShoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartViewModel);
        }
        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId);
            _unitOfWork.ShoppingCart.incrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Summary()
        {
            // To get user ID
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartViewModel = new ShoppingCartViewModel()
            {
                ListCart = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == claim.Value, includeproperties: "Product"),
                OrderHeader = new OrderHeader()
            };

            // To get all data like Ph no. Address from Identity User
            ShoppingCartViewModel.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == claim.Value);

            ShoppingCartViewModel.OrderHeader.Name = ShoppingCartViewModel.OrderHeader.ApplicationUser.Name;
            ShoppingCartViewModel.OrderHeader.PhoneNumber = ShoppingCartViewModel.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartViewModel.OrderHeader.StreetAddress = ShoppingCartViewModel.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartViewModel.OrderHeader.State = ShoppingCartViewModel.OrderHeader.ApplicationUser.State;
            ShoppingCartViewModel.OrderHeader.City = ShoppingCartViewModel.OrderHeader.ApplicationUser.City;
            ShoppingCartViewModel.OrderHeader.PostalCode = ShoppingCartViewModel.OrderHeader.ApplicationUser.PostalCode;

            foreach (var cart in ShoppingCartViewModel.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price);
                ShoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(ShoppingCartViewModel);
        }

        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPost(ShoppingCartViewModel ShoppingCartViewModel)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                ShoppingCartViewModel.ListCart = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == claim.Value, includeproperties: "Product");
                // for Payment Status
                ShoppingCartViewModel.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
                ShoppingCartViewModel.OrderHeader.OrderStatus = SD.StatusPending;
                ShoppingCartViewModel.OrderHeader.OrderDate = DateTime.Now;
                ShoppingCartViewModel.OrderHeader.ApplicationUserId = claim.Value;
            }
            else
            {
                return NotFound();
            }
            //Calculation order total
            foreach (var cart in ShoppingCartViewModel.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price);
                ShoppingCartViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }
            _unitOfWork.OrderHeader.Add(ShoppingCartViewModel.OrderHeader);
            _unitOfWork.Save();
            foreach (var cart in ShoppingCartViewModel.ListCart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductId = cart.ProductId,
                    OrderId = ShoppingCartViewModel.OrderHeader.Id, // This will be acchived from the above code when we save the order header new id will generate
                    Price = cart.Price,
                    Count = cart.Count

                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();

                //Stripe Configuration 

                var domain = "https://localhost:44337/";
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    LineItems = new List<SessionLineItemOptions>()
                    ,
                    Mode = "payment",
                    SuccessUrl = domain+$"customer/cart/OrderConfirmation?id={ShoppingCartViewModel.OrderHeader.Id}",
                    CancelUrl = domain+$"customer/cart/Index",
                };

                foreach(var item in ShoppingCartViewModel.ListCart)
                {
                  var sessionLineItem =  new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount =(long)(item.Price*100), //20.00 to 2000
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name,
                                 Description= item.Product.Description  
                            },

                        },
                        Quantity = item.Count,
                    };
                    options.LineItems.Add(sessionLineItem);

                }
                // Creating a new session
                var service = new SessionService();
                Session session = service.Create(options);
                _unitOfWork.OrderHeader.UpdateStripePaymentId(ShoppingCartViewModel.OrderHeader.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);


                // After we have saved and done with the order . We have to clear the cart
                //_unitOfWork.ShoppingCart.RemoveRange(ShoppingCartViewModel.ListCart);
                //_unitOfWork.Save();
            }
            return RedirectToAction("Index","Home");
        }
        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(x => x.Id == id);
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);
            // check the stripe payment session
            if(session.PaymentStatus.ToLower()== "paid")
            {
                _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                _unitOfWork.Save();
            }
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == orderHeader.ApplicationUserId).ToList();
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            _unitOfWork.Save();
            return View(id);

        }
        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId);
            if(cart.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
                var count = _unitOfWork.ShoppingCart.GetAll(u=>u.ApplicationUserId == cart.ApplicationUserId).ToList().Count-1;
                HttpContext.Session.SetInt32(SD.SessionCart, count);
            }
            else 
            { 
            _unitOfWork.ShoppingCart.decrementCount(cart, 1);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(x => x.Id == cartId);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            var count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.SessionCart, count);
            return RedirectToAction("Index");
        }

        private double GetPriceBasedOnQuantity(double quantity,double price)
        {
            return price;
        }
    }
}
