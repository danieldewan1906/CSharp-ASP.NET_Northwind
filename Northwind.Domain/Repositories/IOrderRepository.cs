using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrder(bool trackChanges);

        Task<Order> GetOrderById(int OrdersId, bool trackChanges);

        void Insert(Order Orders);

        void Edit(Order Orders);

        void Remove(Order Orders);
    }
}
