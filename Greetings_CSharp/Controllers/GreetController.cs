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
        private ICreateReadUpdateDelete crud;
        
        public GreetController([FromServices] ICreateReadUpdateDelete createReadUpdateDelete){
            crud = createReadUpdateDelete; 
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            
            TempData["count"] = crud.CountData();
            return View();
        }

        public IActionResult Greeted()
        {
            var grettingsDatabase = crud.GreetedNames();
            return View(grettingsDatabase);
        }

        public IActionResult Delete(int? id)
        {
            
            crud.DeleteName(id);
            return RedirectToAction("Greeted");
        }

        public IActionResult GreetedName(int? id)
        {
            var obj = crud.FindUserById(id);
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
            if(crud.CountData() > 0){
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
            
            var regexTrue = Regex.IsMatch(bindedData.Name, @"^[a-zA-Z]+$");
            bindedData.Name = crud.CapitalizeFirstLetterAndLowerRest(bindedData.Name);
            if(!String.IsNullOrEmpty(bindedData.Name) && !String.IsNullOrEmpty(language) && regexTrue){
                TempData["message"] = crud.CreateAndUpdate(bindedData, language);;
            } else{
                TempData["errorMessage"] = message.Message(bindedData, language);
            }
            return RedirectToAction("Index");
        }
    }
}
