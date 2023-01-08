using AppShop.Data;
using AppShop.Models;
using AppShop.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;

namespace AppShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository<ProductImage> repository;
        private readonly AppShopContext _context;

        public AdminController(IRepository<ProductImage> repository, AppShopContext context)
        {
            this.repository = repository;
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Upload
        [HttpGet]
        public IActionResult AddImages()
        {
            ViewBag.Products = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Product, "Id", "Title");
            return View();
        }

        [HttpPost]
        public IActionResult AddImages(ProductImage pimage, List<IFormFile> Picture)
        {
            if (Picture == null) return View();
            List<ProductImage> list = new List<ProductImage>();
            foreach (var item in Picture)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    item.CopyTo(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    ProductImage pi = new ProductImage { ProductId = pimage.ProductId, Picture = ms.ToArray() };
                    list.Add(pi);
                }
            }
            _context.ProductImages.AddRange(list);
            _context.SaveChanges();
            return View();
        }
    }
}
