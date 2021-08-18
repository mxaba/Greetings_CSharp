using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Greetings_CSharp.Database;
using Greetings_CSharp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Greetings_CSharp.Controllers
{
    public class GreetController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GreetController(ApplicationDbContext db){
            _db = db;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Greetings> objectList = _db.Greetings;
            TempData["count"] = objectList.Count();
            return View();
        }

        public IActionResult Greeted()
        {
            IEnumerable<Greetings> objectList = _db.Greetings;
            return View(objectList);
        }

        public IActionResult Delete(int? id)
        {
            var crud = new CRUD(_db);
            crud.DeleteName(id);
            return RedirectToAction("Greeted");
        }

        public IActionResult Reset()
        {
            var crud = new CRUD(_db);
            IEnumerable<Greetings> objectList = _db.Greetings;
            if(objectList.Count() > 0){
                crud.ResetDB();
                TempData["errorMessage"] = "Names deleted 🤯";
            }else {
                TempData["errorMessage"] = "Nothing to be deleted 😅";
            }
            
            return RedirectToAction("index");
        }

        // POST: /<controller>/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GreetMe(Greetings objList, string language)
        {
            var message = new GreetMessage();
            var crud = new CRUD(_db);
            var regexTrue = Regex.IsMatch(objList.Name, @"^[a-zA-Z]+$");
            if(!String.IsNullOrEmpty(objList.Name) && !String.IsNullOrEmpty(language) && regexTrue){
                crud.CreteAndUpdate(objList, language);
                TempData["message"] = message.Message(objList, language);
            } else{
                TempData["errorMessage"] = message.ErrorMessage(objList, language);
            }
            return RedirectToAction("Index");
        }
    }
}
