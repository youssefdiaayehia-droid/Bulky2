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
            var products = _unitOfWork.Product.GetAll().ToList();

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
                if (file != null )
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"Images\Product");


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
                product = (Product)_unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.CategoryId.ToString()
                });
                return View(product);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product product = _unitOfWork.Product.Get(u => u.ProductId == id);

            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            if (product == null )
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Product.Save();
            return RedirectToAction("Index");
        }
    }
}
