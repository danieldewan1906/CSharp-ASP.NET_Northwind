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
        public ProductDto productDto { get; set; }

        [Required(ErrorMessage = "Please Insert Photo")]
        public List<IFormFile> AllPhoto { get; set; }

    }
}
