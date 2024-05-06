using EMart.Models;
using EMart.Respository;
using EMart.Respository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EMart.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        
        private readonly IUnitofWork _unitofWork;

        public HomeController(IUnitofWork db)
        {
            _unitofWork = db;
        }

        public IActionResult Index()
        {
            var productList = _unitofWork.ProductRepository.GetAll(IncludeProperties: "Category").ToList();
            return View(productList);
        }

        public IActionResult ProductDetails(int id)
        {
           var product = _unitofWork.ProductRepository.Get(u => u.Id == id, IncludeProperties: "Category");
            return View(product);
        }
        public IActionResult ProductsByCategory(int id)
        {
            var allProducts = _unitofWork.ProductRepository.GetAll(IncludeProperties: "Category").ToList();
            var productList = allProducts.Where(p => p.CategoryId == id).ToList();
            return View(productList);
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
