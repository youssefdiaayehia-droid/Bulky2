using Bulky2_Models;
using Bulky2_DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Bulky2_DataAccess.Repository.IRepository;

namespace Bulky2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public CategoryController(IUnitOfWork db)
        {
            unitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Category> categories = unitOfWork.Category.GetAll().ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Add(category);
                unitOfWork.Category.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = unitOfWork.Category.Get(u=> u.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Update(category);
                unitOfWork.Category.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = unitOfWork.Category.Get(u=>u.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }
            return View();
        }
        [HttpPost,  ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? category = unitOfWork.Category.Get(u=> u.CategoryId==id);
            if (category == null)
            {
                return NotFound();
            }
            unitOfWork.Category.Remove(category);
            unitOfWork.Category.Save();
            return RedirectToAction("Index");
        }

    }
}
