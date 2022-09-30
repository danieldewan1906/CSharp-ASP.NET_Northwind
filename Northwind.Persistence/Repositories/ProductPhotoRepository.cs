using Microsoft.EntityFrameworkCore;
using Northwind.Domain.Models;
using Northwind.Domain.Repositories;
using Northwind.Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Persistence.Repositories
{
    public class ProductPhotoRepository : RepositoryBase<ProductPhoto>, IProductPhotoPhotoRepository
    {
        public ProductPhotoRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void Edit(ProductPhoto ProductPhoto)
        {
            Update(ProductPhoto);
        }

        public async Task<IEnumerable<ProductPhoto>> GetAllProductPhoto(bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(p => p.PhotoId)
                .Include(s => s.PhotoProduct)
                .ToListAsync();
        }

        public async Task<ProductPhoto> GetProductPhotoById(int ProductPhotoId, bool trackChanges)
        {
            return await FindByCondition(p => p.PhotoProductId.Equals(ProductPhotoId), trackChanges)
                .Include(s => s.PhotoProduct)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductPhoto>> GetProductPhotoPaged(int pageIndex, int pageSize, bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(p => p.PhotoProductId)
                .Include(s => s.PhotoProduct)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public void Insert(ProductPhoto ProductPhoto)
        {
            Create(ProductPhoto);
        }

        public void Remove(ProductPhoto ProductPhoto)
        {
            Delete(ProductPhoto);
        }
    }
}
