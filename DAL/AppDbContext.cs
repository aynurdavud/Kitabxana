using KitabxanaS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitabxanaS.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Home> Homes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<About> Abouts { get; set; }
    }
}
