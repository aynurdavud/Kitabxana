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
    public class AboutController : Controller
    {

        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public AboutController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            About about = await _db.Abouts.FirstOrDefaultAsync();
            return View(about);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            About about = await _db.Abouts.FirstOrDefaultAsync(x => x.Id == id);
            if (about == null)
            {
                return NotFound();
            }

            return View(about);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, About changedabout)
        {
            if (id == null)
            {
                return NotFound();
            }
            About dbabout = await _db.Abouts.FirstOrDefaultAsync(x => x.Id == id);
            if (dbabout == null)
            {
                return BadRequest();
            }

            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            if (changedabout.Photo != null)
            {
                if (!changedabout.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please Select image file");

                    return View();
                }

                if (changedabout.Photo.IsMore4Mb())
                {
                    ModelState.AddModelError("Photo", "Image max 4 mb");

                    return View();
                }

                string path = Path.Combine(_env.WebRootPath, "img");
                dbabout.Image = await changedabout.Photo.SaveImageAsync(path);

                return View();
            }
            dbabout.Title = changedabout.Title;
            dbabout.Description = changedabout.Description;
            dbabout.Link = changedabout.Link;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
