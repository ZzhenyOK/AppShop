using AppShop.Data;
using AppShop.Models;
using AppShop.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AppShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppShopContext _context;
        private const string LOGGED_USER = "LoggedUser";
        public UsersController(AppShopContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Registration
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //AddToCart
        [HttpGet]
        public IActionResult AddToCart(int? id)
        {
            int? loggegUserId = 2; //// HttpContext.Session.GetInt32(LOGGED_USER);
            //if(loggegUserId == null)
            //{
            //    return RedirectToAction("Login");
            //}
            if(id != null)
            {
                Cart cart = new Cart { ProductId = (int)id, UserId = (int)loggegUserId, Qty = 1 };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }
            List<Cart> selectedCart = _context.Carts.Where(c => c.UserId == (int)loggegUserId).ToList();
            List<Product> products = new List<Product>();
            foreach(Cart item in selectedCart)
            {
                Product product = _context.Product.Include(p => p.Vendor).
                    Where(p => p.Id == item.ProductId).FirstOrDefault();
                product.Qty = item.Qty;
                products.Add(product);
            }
            return View(products);
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (user == null) RedirectToAction("Register");
            user.Password = GetHash(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Index", "Products");
        }

        //Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            //if login is successful -> RedirectToAction("Success");
            //if login is unsuccessful -> RedirectToAction("Register");
            //Add new value to Session
            HttpContext.Session.SetInt32(LOGGED_USER, 2);
            return RedirectToAction("Index", "Products");
        }

        public string GetHash(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }

        [HttpPost]
        public IActionResult ChangeCart(string productId, string Qty)
        {
            int prodId = Int32.Parse(productId);
            int qty = Int32.Parse(Qty);
            int? userId = HttpContext.Session.GetInt32(LOGGED_USER);
            var cart = _context.Carts.Where(c => c.ProductId == prodId &&
                c.UserId == (int)userId).FirstOrDefault();
            cart.Qty = qty;
            _context.Entry<Cart>(cart).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("AddToCart");
        }

        [HttpPost]
        public void ChangeCartAjax(string productId, string Qty)
        {
            int prodId = Int32.Parse(productId);
            int qty = Int32.Parse(Qty);
            int? userId = HttpContext.Session.GetInt32(LOGGED_USER);
            var cart = _context.Carts.Where(c => c.ProductId == prodId &&
                c.UserId == (int)userId).FirstOrDefault();
            cart.Qty = qty;
            _context.Entry<Cart>(cart).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
