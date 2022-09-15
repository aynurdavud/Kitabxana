
using KitabxanaS.DAL;
using KitabxanaS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KitabxanaS.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;

        }

        public IActionResult Index()
        {
            Home home = _db.Homes.FirstOrDefault();
            return View(home);
        }

      
        public IActionResult Error()
        {
            return View();
        }
    }
}
