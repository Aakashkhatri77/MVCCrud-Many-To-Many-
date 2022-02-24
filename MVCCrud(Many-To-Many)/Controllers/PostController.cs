using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCCrud_Many_To_Many_.Models;

namespace MVCCrud_Many_To_Many_.Context
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: PostController
        public ActionResult Index()
        {
            var post = _context.Posts.Include(p=>p.PostCategories).ThenInclude(c=>c.Category).ToList();
            return View(post);
        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            var posts = _context.Posts.Find(id);
            return View(posts);
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            ViewData["categories"] = _context.Categories.ToList();
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post, int[] selectedCategoryIds)
        {
            try
            {
                foreach (var item in selectedCategoryIds)
                {
                    post.PostCategories.Add(new PostCategory { CategoryId = item }) ;
                }
                _context.Add(post);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewData["categories"] = _context.Categories.ToList();
            var post = _context.Posts.Include(p=>p.PostCategories).FirstOrDefault(x=>x.Id == id);
            return View(post);
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Post post, int[] selectedCategoryIds)
        {
            try
            {
                var data = _context.Posts.Include(p => p.PostCategories).FirstOrDefault(x => x.Id == id);
                //Remove selected Category
                var removeCategories = new List<PostCategory>();
                foreach (var postCategories in data.PostCategories)
                {
                    if (!selectedCategoryIds.Contains(postCategories.CategoryId))
                    {
                        removeCategories.Add(postCategories);
                    }
                }

                //Remove Old Select
                foreach (var postCateogry in removeCategories)
                {
                    _context.PostCategories.Remove(postCateogry);
                }

                //Add new selected Category
                foreach (var item in selectedCategoryIds)
                {
                    if (!data.PostCategories.Any(pc=>pc.CategoryId == item))
                    {
                        data.PostCategories.Add(new PostCategory { CategoryId = item, PostId = post.Id });
                    }
                }
                data.Title = post.Title;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            var post = _context.Posts.Find(id);
            return View(post);
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var post = _context.Posts.Find(id);
                if (post != null) { 
                _context.Remove(post);
                _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
