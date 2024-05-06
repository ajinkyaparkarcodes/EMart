using EMart.Models;
using EMart.Respository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;

namespace EMart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitofWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitofWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var productList = _unitOfWork.ProductRepository.GetAll(IncludeProperties:"Category").ToList();
            return View(productList);
        }

        public IActionResult AddProduct()
        {
            var productViewModel = new ProductViewModel
            {
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                }),
                Product = new Product()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel model, IFormFile imageFile)
        {
            if (model.Product.Name == model.Product.ListPrice.ToString())
            {
                ModelState.AddModelError("Name", "Product Name and Price cannot be the same");
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (imageFile != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"Images\Product");

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    model.Product.ImageUrl = @"\Images\Product\" + fileName;
                }      

                _unitOfWork.ProductRepository.Add(model.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product added successfully";
                return RedirectToAction("Index");
            }
            else
            {
                model.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
                return View(model);
            }
        }

        public IActionResult EditProduct(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var productViewModel = new ProductViewModel
            {
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                }),
                Product = product
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult EditProduct(ProductViewModel model, IFormFile? imageFile)
        {
            if (model.Product.Name == model.Product.ListPrice.ToString())
            {
                ModelState.AddModelError("Name", "Product Name and Price cannot be the same");
            }

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (imageFile != null)
                {
                   
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                    string productPath = Path.Combine(wwwRootPath, @"Images\Product");

                    if (!string.IsNullOrEmpty(model.Product.ImageUrl))
                    {
                        var existingImagePath = Path.Combine(wwwRootPath, model.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(existingImagePath))
                        {
                            System.IO.File.Delete(existingImagePath);
                        }
                    }

                   
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    model.Product.ImageUrl = @"\Images\Product\" + fileName;
                }

                _unitOfWork.ProductRepository.Update(model.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            else
            {
                model.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
                return View(model);
            }
        }


        public IActionResult DeleteProduct(int id)
        {
            var product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.ProductRepository.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() 
        {
            var productList = _unitOfWork.ProductRepository.GetAll(IncludeProperties: "Category").ToList();
            return Json(new {data = productList});
        }



        #endregion
    }
}
