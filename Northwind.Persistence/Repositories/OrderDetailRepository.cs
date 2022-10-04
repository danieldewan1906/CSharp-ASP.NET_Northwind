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

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetail(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(x => x.ProductId)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<OrderDetail> GetOrderDetailById(int OrderDetailsId, bool trackChanges)
        {
            return await FindByCondition(x => x.ProductId.Equals(OrderDetailsId), trackChanges)
                .Include(p => p.Product)
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
