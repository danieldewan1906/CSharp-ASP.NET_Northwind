using Northwind.Contracts.Dto.Order;
using Northwind.Contracts.Dto.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contracts.Dto.Product
{
    public class ProductOrderGroupDto
    {
        public ProductDto productDto { get; set; }
        public OrderDto orderDto { get; set; }
        public OrderDetailDto orderDetailDto { get; set; }
    }
}
