using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ItemsController : Controller
    {
       
        private readonly AppDbContext _db;
        public ItemsController (AppDbContext db)
        {  _db = db; }
        public IActionResult Index()
        {
            IEnumerable<Item> itemList= _db.Items.Include(c=>c.Category).ToList();
            
            return View(itemList);
        }
        //Get
        public IActionResult New()
        {

            createSelectList();
            return View();
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item item)
        {
            if (ModelState.IsValid)
            {
                _db.Items.Add(item);
                _db.SaveChanges();
                TempData["message"] = "Item Added Succsessfully"; 
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
            
        }
        public void createSelectList(int selectId=1)
        {
            //List<Category> categories = new List<Category> {
            //    new Category { Id = 0,Name="Select Category"},
            //    new Category { Id = 1,Name="Computer"},
            //    new Category { Id = 2,Name="Mobiles"},
            //    new Category { Id = 3,Name="Electroic Device"}

            //};
            List<Category> categories= _db.Categories.ToList();
            SelectList listItems=new SelectList(categories,"Id","Name",selectId);
            ViewBag.CategoryList = listItems;
        }
        //Get
        public IActionResult Edit(int?Id)
        {
            if(Id==0 || Id==null)
            {
                return NotFound();
            }
            var item=_db.Items.Find(Id);
            if (item==null)
            {
                return NotFound();
            }
            createSelectList(item.CategoryId);
            return View(item);
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                _db.Items.Update(item);
                _db.SaveChanges();
                TempData["message"] = "Item Updated Succsessfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }

        }
        //Get
        public IActionResult Delete(int? Id)
        {
            if (Id == 0 || Id == null)
            {
                return NotFound();
            }
            var item = _db.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
            createSelectList(item.CategoryId);
            return View(item);
        }
        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int? Id)
        {
            var item = _db.Items.Find(Id);
            if (item == null)
            {
                return NotFound();
            }
            _db.Items.Remove(item);
            _db.SaveChanges();
            TempData["message"] = "Item Deleted Succsessfully";
            return RedirectToAction("Index");
          
        }
    }
}


