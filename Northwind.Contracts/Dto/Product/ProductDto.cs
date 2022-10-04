using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Contracts.Dto.Supplier;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contracts.Dto.Product
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        [Display(Name = "Quantity Per Unit")]
        public string QuantityPerUnit { get; set; }

        [Display(Name = "Unit Price")]
        public decimal? UnitPrice { get; set; }

        [Display(Name = "Unit Stock")]
        public short? UnitsInStock { get; set; }
        [Display(Name = "Unit On Order")]
        public short? UnitsOnOrder { get; set; }
        [Display(Name = "Reorder Level")]
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public virtual CategoryDto Category { get; set; }
        public virtual SupplierDto Supplier { get; set; }
        [Display(Name = "Product Photo")]
        public virtual ICollection<OrderDetailDto> OrderDetails { get; set; }
        public virtual ICollection<ProductPhotoDto> ProductPhotos { get; set; }
    }
}
