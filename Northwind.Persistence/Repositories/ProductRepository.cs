using Microsoft.EntityFrameworkCore;
using Northwind.Domain.Base;
using Northwind.Domain.Models;
using Northwind.Domain.Repositories;
using Northwind.Persistence.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void Edit(Product product)
        {
            Update(product);
        }

        public async Task<IEnumerable<Product>> GetAllProduct(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(p => p.ProductId)
                .Include(c => c.Category)
                .Include(s => s.Supplier)
                .Include(od => od.OrderDetails)
                .ToListAsync();
        }


        public async Task<Product> GetProductById(int productId, bool trackChanges)
        {
            return await FindByCondition(p => p.ProductId.Equals(productId), trackChanges)
                .Include(c => c.Category)
                .Include(s => s.Supplier)
                .Include(od => od.OrderDetails)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductOnSales(bool trackChanges)
        {
            /*select * from products p 
             * where exists(select * from ProductPhotos pp 
             * where pp.PhotoProductId = p.ProductID)*/

            // diatas query SQL dibawah query by c#

            var product = await FindAll(trackChanges)
                .Where(x => x.ProductPhotos.Any(y => y.PhotoProductId == x.ProductId))
                .Include(p => p.ProductPhotos)
                .ToListAsync();
            return product;
        }

        public async Task<Product> GetProductPhotoOnSalesById(int productId, bool trackChanges)
        {
            var products = await FindByCondition(x => x.ProductId.Equals(productId), trackChanges)
                .Where(y => y.ProductPhotos.Any(p => p.PhotoProductId == productId))
                .Include(c => c.Category)
                .Include(s => s.Supplier)
                .Include(a => a.ProductPhotos)
                .Include(od => od.OrderDetails)
                .SingleOrDefaultAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(p => p.ProductId)
                .Include(c => c.Category)
                .Include(s => s.Supplier)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void Insert(Product product)
        {
            Create(product);
        }

        public void Remove(Product product)
        {
            Delete(product);
        }

        public async Task<Product> GetProductOrderOnSalesById(int productId, bool trackChanges)
        {
            var products = await FindByCondition(x => x.ProductId.Equals(productId), trackChanges)
                .Where(y => y.ProductPhotos.Any(p => p.PhotoProductId == productId))
                .Include(c => c.Category)
                .Include(s => s.Supplier)
                .Include(o => o.OrderDetails)
                .Include(a => a.ProductPhotos)
                .SingleOrDefaultAsync();
            return products;
        }
    }
}
