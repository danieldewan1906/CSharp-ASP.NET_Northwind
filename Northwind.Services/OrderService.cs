using AutoMapper;
using Northwind.Contracts.Dto.Order;
using Northwind.Contracts.Dto.OrderDetail;
using Northwind.Domain.Base;
using Northwind.Domain.Entities;
using Northwind.Domain.Models;
using Northwind.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public OrderService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(OrderDto OrderDto)
        {
            var edit = _mapper.Map<Order>(OrderDto);
            _repositoryManager.OrderRepository.Edit(edit);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrder(bool trackChanges)
        {
            var order = await _repositoryManager.OrderRepository.GetAllOrder(trackChanges);
            // source = categoryModel, target = CategoryDto
            var orderDto = _mapper.Map<IEnumerable<OrderDto>>(order);
            return orderDto;
        }

        public async Task<OrderDto> GetOrderById(int orderId, bool trackChanges)
        {
            var model = await _repositoryManager.OrderRepository.GetOrderById(orderId, trackChanges);
            var dto = _mapper.Map<OrderDto>(model);
            return dto;
        }

        public void Insert(OrderForCreateDto orderForCreateDto)
        {
            var newData = _mapper.Map<Order>(orderForCreateDto);
            _repositoryManager.OrderRepository.Insert(newData);
            _repositoryManager.Save();
        }

        public void Remove(OrderDto OrderDto)
        {
            var delete = _mapper.Map<Order>(OrderDto);
            _repositoryManager.OrderRepository.Remove(delete);
            _repositoryManager.Save();
        }
    }
}
