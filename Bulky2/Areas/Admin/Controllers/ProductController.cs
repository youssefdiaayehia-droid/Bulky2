using Bulky2_DataAccess.Repository.IRepository;
using Bulky2_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bulky2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _WebHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return View(products);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
                .Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
            ViewBag.CategoryList = CategoryList;
            if (id == null || id == 0)
            {
                return View(new Product());
            }
            else
            {
                Product product = _unitOfWork.Product.Get(u => u.ProductId == id);
                return View(product);
            }

        }
        [HttpPost]
        public IActionResult Upsert(Product product, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _WebHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"Images\Product");

                    // --- ADD THIS CHECK ---
                    if (!Directory.Exists(productPath))
                    {
                        Directory.CreateDirectory(productPath);
                    }
                    // ----------------------

                    using (var fileStrream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStrream);
                    }
                    product.ImageUrl = @"\Images\Product\" + fileName;
                }
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll()
                    .Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.CategoryId.ToString()
                    });
                ViewBag.CategoryList = CategoryList;
                return View(product);
            }
        }
        



        #region API CALLS
            [HttpGet]
            public IActionResult GetAll()
            {
                var products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
                return Json(new { data = products });
            }
        public IActionResult Delet(int? id)
        {
            var ProductToBeDeleted = _unitOfWork.Product.Get(u => u.ProductId == id);
            if (ProductToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var OldImagePath = Path.Combine(_WebHostEnvironment.WebRootPath,
                ProductToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(OldImagePath))
            {
                System.IO.File.Delete(OldImagePath);
            }

            _unitOfWork.Product.Remove(ProductToBeDeleted);
            _unitOfWork.Product.Save();

            return Json(new { success = true, message = "Delete Successful" });

        }

        #endregion
    }
}