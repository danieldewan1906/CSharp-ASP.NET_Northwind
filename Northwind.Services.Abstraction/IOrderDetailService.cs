using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetailDto>> GetAllOrderDetail(bool trackChanges);
        Task<IEnumerable<OrderDetailDto>> GetAllCartItem(string custId, bool trackChanges);
        Task<OrderDetailDto> GetOrderDetail(int orderId, int productId, bool trackChanges);
        Task<OrderDetailDto> GetOrderDetailById(int orderId, bool trackChanges);

        void Insert(OrderDetailForCreateDto OrderDetailForCreateDto);

        void Edit(OrderDetailDto OrderDetailDto);

        void Remove(OrderDetailDto OrderDetailDto);
    }
}
