using Microsoft.AspNetCore.Mvc;
using MVCCrud_Many_To_Many_.Models;

namespace MVCCrud_Many_To_Many_.Context
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }

        //Get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Details
        public IActionResult Details(int id)
        {
            var category = _context.Categories.Find(id);
            return View(category);
        }


        //Edit
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit (int id, Category category)
        {
            var data = _context.Categories.Find(id);
            data.Title = category.Title;
            _context.Categories.Update(data);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Delete
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(int id, Category category)
        {
            var _category = _context.Categories.Find(id);
            _context.Categories.Remove(_category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
