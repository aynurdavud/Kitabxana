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
    public class BooksController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public BooksController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Book> book = await _db.Books.ToListAsync();
            return View(book);
           
        }
        public async Task<IActionResult> Create()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid)
            {

                return View();
            };
            if (book.Photo != null)
            {

                if (!book.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please choose the ImageFile!");

                    return View();
                }

                if (book.Photo.IsMore4Mb())
                {
                    ModelState.AddModelError("Photo", "Image Max 4MB!");

                    return View();

                }
                string path = Path.Combine(_env.WebRootPath, "img");
                book.Image = await book.Photo.SaveImageAsync(path);
            }
          
            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Book book = await _db.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Book changedbook)
        {
            if (id == null)
            {
                return NotFound();
            }
            Book dbbook = await _db.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (dbbook == null)
            {
                return BadRequest();
            }

            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            if (changedbook.Photo != null )
            {
                if (!changedbook.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please Select image file");

                    return View();
                }

                if (changedbook.Photo.IsMore4Mb())
                {
                    ModelState.AddModelError("Photo", "Image max 4 mb");

                    return View();
                }

                string path = Path.Combine(_env.WebRootPath, "img");
                dbbook.Image = await changedbook.Photo.SaveImageAsync(path);
              
                return View();
            }



            dbbook.Title = changedbook.Title;
            dbbook.Description = changedbook.Description;
            
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
