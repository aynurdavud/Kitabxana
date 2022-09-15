using KitabxanaS.DAL;
using KitabxanaS.Helpers;
using KitabxanaS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KitabxanaS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public HomeController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            Home home=await _db.Homes.FirstOrDefaultAsync();
            return View(home);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Home home = await _db.Homes.FirstOrDefaultAsync(x => x.Id == id);
            if (home == null)
            {
                return NotFound();
            }

            return View(home);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Home changedhome)
        {
            if (id == null)
            {
                return NotFound();
            }
            Home dbhome = await _db.Homes.FirstOrDefaultAsync(x => x.Id == id);
            if (dbhome == null)
            {
                return BadRequest();
            }

            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            if (changedhome.Photo1 != null && changedhome.Photo2 != null)
            {
                if (!changedhome.Photo1.IsImage()&& !changedhome.Photo2.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please Select image file");

                    return View();
                }

                if (changedhome.Photo1.IsMore4Mb()&& changedhome.Photo2.IsMore4Mb())
                {
                    ModelState.AddModelError("Photo", "Image max 4 mb");

                    return View();
                }

                string path = Path.Combine(_env.WebRootPath, "img");
                dbhome.Image1 = await changedhome.Photo1.SaveImageAsync(path);
                dbhome.Image2 = await changedhome.Photo2.SaveImageAsync(path);
                return View();
            }
           

               
            dbhome.Title1 = changedhome.Title1;
            dbhome.Description1 = changedhome.Description1;
            dbhome.Title2 = changedhome.Title2;
            dbhome.Description2 = changedhome.Description2;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
