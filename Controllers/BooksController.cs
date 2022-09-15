using KitabxanaS.DAL;
using KitabxanaS.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitabxanaS.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _db;
        public BooksController(AppDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            List<Book> book = _db.Books.ToList();
            return View(book);
        }
    }
}
