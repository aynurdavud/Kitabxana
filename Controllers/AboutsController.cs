using KitabxanaS.DAL;
using KitabxanaS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitabxanaS.Controllers
{
    public class AboutsController : Controller
    {
        private readonly AppDbContext _db;
        public AboutsController(AppDbContext db)
        {
            _db = db;

        }

        public IActionResult Index()
        {
            About about = _db.Abouts.FirstOrDefault();
            return View(about);
        }
    }
}
