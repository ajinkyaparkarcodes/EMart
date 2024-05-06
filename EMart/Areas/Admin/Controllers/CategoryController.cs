using EMart.Models;
using EMart.Respository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =Roles.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitofwork;
        public CategoryController(IUnitofWork db)
        {
            _unitofwork = db;
        }
        public IActionResult Index()
        {
            var CategoryList = _unitofwork.CategoryRepository.GetAll().ToList();
            return View(CategoryList);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(Category C)
        {
            if (C.Name == C.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Name cannot be same as the Display Order");
            }
            if (C.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is invalid value");
            }
            if (ModelState.IsValid)
            {
                _unitofwork.CategoryRepository.Add(C);
                _unitofwork.Save();
                TempData["success"] = "Category Created Successfuly";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            var data = _unitofwork.CategoryRepository.Get(u => u.CategoryId == id);
            if (id == null || id == 0)
            {
                return NotFound();
            }
            if (data == null)
            {
                return NotFound();
            }
            return View(data);
        }

        [HttpPost]
        public IActionResult EditCategory(Category c)
        {
            if (ModelState.IsValid)
            {
                _unitofwork.CategoryRepository.Update(c);
                _unitofwork.Save();
                TempData["success"] = "Category Updated Successfuly";
                return RedirectToAction("Index");
            }
            return View();
        }


        public IActionResult DeleteCategory(int id)
        {
            var data = _unitofwork.CategoryRepository.Get(u => u.CategoryId == id);
            if (data == null)
            {
                return NotFound();
            }
            _unitofwork.CategoryRepository.Remove(data);
            _unitofwork.Save();
            TempData["success"] = "Category Deleted Successfuly";
            return RedirectToAction("Index");
        }
    }
}
