using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Greetings_CSharp.Models;
using Greetings_CSharp.Database;

namespace Greetings_CSharp.Controllers
{
    public class NotFoundController : Controller
    {
        private readonly ApplicationDbContext _db;

        public NotFoundController(ApplicationDbContext db){
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
