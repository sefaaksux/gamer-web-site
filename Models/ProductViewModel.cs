using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gallery.Models
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Ürün adı gereklidir.")]
        public string Name { get; set; }

        public string Description { get; set; }

        // Diğer özellikler eklenebilir

        [Display(Name = "Resimler")]
        public List<IFormFile> Images { get; set; }

        public int CategoryId { get; set; } 
    }
}
