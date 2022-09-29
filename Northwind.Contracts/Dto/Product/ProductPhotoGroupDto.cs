using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contracts.Dto.Product
{
    public class ProductPhotoGroupDto
    {
        public ProductForCreateDto productForCreateDto { get; set; }

        [Display(Name = "Photo 1")]
        public IFormFile Photo1 { get; set; }

        [Display(Name = "Photo 2")]
        public IFormFile Photo2 { get; set; }

        [Display(Name = "Photo 3")]
        public IFormFile Photo3 { get; set; }

        [Required(ErrorMessage = "Please Insert Photo")]
        public List<IFormFile> AllPhoto { get; set; }



    }
}
