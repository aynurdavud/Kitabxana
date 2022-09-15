using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KitabxanaS.Models
{
    public class Home
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu xana bos  ola bilmez")]
        public string Title1 { get; set; }
        [Required(ErrorMessage = "Bu xana bos  ola bilmez")]
        public string Title2 { get; set; }
        [Required(ErrorMessage = "Bu xana bos  ola bilmez")]
        public string Image1 { get; set; }
       
        public string Image2 { get; set; }
      
        public string Description1 { get; set; }
        [Required(ErrorMessage = "Bu xana bos  ola bilmez")]
        public string Description2 { get; set; }
        [Required(ErrorMessage = "Bu xana bos  ola bilmez")]
        public string AboutUs { get; set; }

        [NotMapped]
        public IFormFile Photo1 { get; set; }

        [NotMapped]
        public IFormFile Photo2 { get; set; }

    }
}
