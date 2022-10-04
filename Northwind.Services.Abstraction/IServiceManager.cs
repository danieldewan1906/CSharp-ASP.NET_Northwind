using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface IServiceManager
    {
        ICategoryService CategoryService { get; }
        ISupplierService SupplierService { get; }
        IProductService ProductService { get; }
        IProductPhotoService ProductPhotoService { get; }
        IOrderDetailService OrderDetailService { get; }
        IOrderService OrderService { get; }
    }
}
