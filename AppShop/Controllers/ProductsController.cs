using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppShop.Data;
using AppShop.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;
using AppShop.Repository;

namespace AppShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> repository;
        private readonly AppShopContext _context;

        public ProductsController(IRepository<Product> repository, AppShopContext context)
        {
            this.repository = repository;
            this._context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var appShopContext = await repository.GetAllAsync();
            return View(appShopContext);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await repository.GetByIdAsync((int)id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Images = _context.ProductImages.Where(p => p.ProductId == id).ToList();
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["VendorId"] = new SelectList(_context.Set<Vendor>(), "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Rate,Price,VendorId,State")] Product product)
        {
            if (ModelState.IsValid)
            {
                await repository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorId"] = new SelectList(_context.Set<Vendor>(), "Id", "Id", product.VendorId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await repository.GetByIdAsync((int)id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["VendorId"] = new SelectList(_context.Set<Vendor>(), "Id", "Id", product.VendorId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Rate,Price,VendorId,State")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    await repository.UpdateAsync(id, product);  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorId"] = new SelectList(_context.Set<Vendor>(), "Id", "Id", product.VendorId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await repository.GetByIdAsync((int)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (repository.GetByIdAsync(id) != null);
        }




        ////Registration
        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Register(User user)
        //{
        //    if (user == null) RedirectToAction("Register");
        //    user.Password = GetHash(user.Password);
        //    _context.Users.Add(user);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        ////Login
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Login(User user)
        //{
        //    //if login is successful -> RedirectToAction("Success");
        //    //if login is unsuccessful -> RedirectToAction("Register");
        //    //Add new value to Session
        //    HttpContext.Session.SetInt32("LoggedUser", 2);
        //    return RedirectToAction("Index");
        //}

        //public string GetHash(string input)
        //{
        //    if (string.IsNullOrEmpty(input)) return input;
        //    var md5 = MD5.Create();
        //    var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

        //    return Convert.ToBase64String(hash);
        //}

        ////Upload
        //[HttpGet]
        //public IActionResult AddImages()
        //{
        //    ViewBag.Products = new SelectList(_context.Product, "Id", "Title");
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult AddImages(ProductImage pimage, List<IFormFile> Picture)
        //{
        //    if(Picture == null) return View();
        //    List<ProductImage> list = new List<ProductImage>();
        //    foreach (var item in Picture)
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            item.CopyTo(ms);
        //            ms.Seek(0, SeekOrigin.Begin);
        //            ProductImage pi = new ProductImage { ProductId = pimage.ProductId, Picture = ms.ToArray() };
        //            list.Add(pi);
        //        }
        //    }
        //    _context.ProductImages.AddRange(list);
        //    _context.SaveChanges();
        //    return View();
        //}
    }
}
