using Northwind.Domain.Dto;
using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Northwind.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProduct(bool trackChanges);

        Task<Product> GetProductById(int productId, bool trackChanges);

        Task<Product> GetProductPhotoOnSalesById(int productId, bool trackChanges);

        Task<Product> GetProductOrderOnSalesById(int productId, bool trackChanges);

        Task<IEnumerable<Product>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges);

        Task<IEnumerable<Product>> GetProductOnSales(bool trackChanges);

        Task<IEnumerable<TotalProductByCategory>> GetTotalProductByCategory();

        Task<IEnumerable<Category>> GetTotalProductCategoryById(string categoryName);

        void Insert(Product product);

        void Edit(Product product);

        void Remove(Product product);
    }
}
