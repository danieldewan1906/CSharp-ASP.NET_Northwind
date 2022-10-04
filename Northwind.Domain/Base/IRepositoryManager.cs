using Northwind.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Base
{
    public interface IRepositoryManager
    {
        ICategoryRepository CategoryRepository { get; }
        ICustomerRepository EmployeeRepository { get; }
        IProductRepository ProductRepository { get; }
        ISupplierRepository SupplierRepository { get; }
        IProductPhotoPhotoRepository ProductPhotoRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IOrderRepository OrderRepository { get; }

        void Save();

        Task SaveAsync();
    }
}
