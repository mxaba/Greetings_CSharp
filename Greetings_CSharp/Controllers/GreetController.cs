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

        private CreateReadUpdateDelete crud;

        private void CrudConstructor(){
            crud = new CreateReadUpdateDelete(_db);
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Greetings> grettingsDatabase = _db.Greetings;
            TempData["count"] = grettingsDatabase.Count();
            return View();
        }

        public IActionResult Greeted()
        {
            IEnumerable<Greetings> grettingsDatabase = _db.Greetings;
            return View(grettingsDatabase);
        }

        public IActionResult Delete(int? id)
        {
            CrudConstructor();
            crud.DeleteName(id);
            return RedirectToAction("Greeted");
        }

        public IActionResult GreetedName(int? id)
        {
            var obj = _db.Greetings.Find(id);
            if (id == null || id == 0 || obj == null)
            {
                return RedirectToAction("Index", "NotFound");
            }
            ViewBag.name = obj.Name;
            ViewBag.isizulu = obj.Isizulu;
            ViewBag.english = obj.English;
            ViewBag.spanish = obj.Spanish;
            ViewBag.count = obj.Counts;
            return View();
        }

        public IActionResult Reset()
        {
            CrudConstructor();
            IEnumerable<Greetings> grettingsDatabase = _db.Greetings;
            if(grettingsDatabase.Count() > 0){
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
        public IActionResult GreetMe(Greetings bindedData, string language)
        {
            var message = new GreetMessage();
            CrudConstructor();
            var regexTrue = Regex.IsMatch(bindedData.Name, @"^[a-zA-Z]+$");
            if(!String.IsNullOrEmpty(bindedData.Name) && !String.IsNullOrEmpty(language) && regexTrue){
                crud.CreteAndUpdate(bindedData, language);
                TempData["message"] = message.Message(bindedData, language);
            } else{
                TempData["errorMessage"] = message.ErrorMessage(bindedData, language);
            }
            return RedirectToAction("Index");
        }
    }
}
