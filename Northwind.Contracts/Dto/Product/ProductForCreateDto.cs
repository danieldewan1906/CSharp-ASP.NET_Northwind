using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contracts.Dto.Product
{
    public class ProductForCreateDto
    {
        [Required(ErrorMessage = "Please insert product name")]
        [Display(Name ="Product Name")]
        [StringLength(50, ErrorMessage = "Product name cannot be longer than 50")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please insert supplier")]
        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }

        [Required(ErrorMessage = "Please insert category")]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }

        [Display(Name = "Price")]
        [Range(10,999999999.00)]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Units In Stock")]
        [Range(1,99)]
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

    }
}
