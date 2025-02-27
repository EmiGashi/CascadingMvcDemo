using CascadingMvcDemo.Data;
using CascadingMvcDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CascadingMvcDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();

            var products = new List<Product>();

            //categories.Add(new Category()
            //{
            //    Id = 0,
            //    CategoryName = "--Select Category--"
            //});

            //products.Add(new Product()
            //{
            //    Id = 0,
            //    Name = "--Select Product"

            //});

            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");
            ViewBag.Products = new SelectList(products, "Id", "Name");

            return View();
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

        public JsonResult GetProductByCategoryId(int categoryId)
        {
            return Json(_context.Products.Where(u=>u.CategoryId == categoryId).ToList());
        }
    }
}
