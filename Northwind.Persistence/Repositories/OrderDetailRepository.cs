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
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(NorthwindContext dbContext) : base(dbContext)
        {
        }

        public void Edit(OrderDetail OrderDetails)
        {
            Update(OrderDetails);
        }

        public async Task<IEnumerable<OrderDetail>> GetAllCartItem(string custId, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .Where(o => o.Order.CustomerId == custId && o.Order.ShippedDate == null &&
                o.Product.ProductPhotos.Any(y => y.PhotoProductId == o.ProductId))
                .Include(o => o.Order)
                .Include(p => p.Product)
                .Include(pp => pp.Product.ProductPhotos)
                .OrderBy(x => x.OrderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetail(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(x => x.ProductId)
                .Include(p => p.Product)
                .Include(o => o.Order)
                .ToListAsync();
        }

        public async Task<OrderDetail> GetOrderDetail(int OrderDetailsId,int productId, bool trackChanges)
        {
            return await FindByCondition(x => x.OrderId.Equals(OrderDetailsId) && x.ProductId.Equals(productId), trackChanges)
                .Include(p => p.Product)
                .Include(o => o.Order)
                .SingleOrDefaultAsync();
        }

        public async Task<OrderDetail> GetOrderDetailById(int OrderDetailsId, bool trackChanges)
        {
            return await FindByCondition(x => x.OrderId.Equals(OrderDetailsId), trackChanges)
                .Include(p => p.Product)
                .Include(o => o.Order)
                .SingleOrDefaultAsync();
        }

        public void Insert(OrderDetail OrderDetails)
        {
            Create(OrderDetails);
        }

        public void Remove(OrderDetail OrderDetails)
        {
            Delete(OrderDetails);
        }
    }
}
