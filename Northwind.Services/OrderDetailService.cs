using AutoMapper;
using Northwind.Contracts.Dto.Category;
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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public OrderDetailService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public void Edit(OrderDetailDto OrderDetailDto)
        {
            var edit = _mapper.Map<OrderDetail>(OrderDetailDto);
            _repositoryManager.OrderDetailRepository.Edit(edit);
            _repositoryManager.Save();
        }

        public async Task<IEnumerable<OrderDetailDto>> GetAllOrderDetail(bool trackChanges)
        {
            var orderDetails = await _repositoryManager.OrderDetailRepository.GetAllOrderDetail(trackChanges);
            // source = categoryModel, target = CategoryDto
            var orderDetailsDto = _mapper.Map<IEnumerable<OrderDetailDto>>(orderDetails);
            return orderDetailsDto;
        }

        public async Task<OrderDetailDto> GetOrderDetail(int orderId, int productId, bool trackChanges)
        {
            var model = await _repositoryManager.OrderDetailRepository.GetOrderDetail(orderId, productId, trackChanges);
            var dto = _mapper.Map<OrderDetailDto>(model);
            return dto;
        }

        public async Task<OrderDetailDto> GetOrderDetailById(int orderId, bool trackChanges)
        {
            var model = await _repositoryManager.OrderDetailRepository.GetOrderDetailById(orderId, trackChanges);
            var dto = _mapper.Map<OrderDetailDto>(model);
            return dto;
        }

        public void Insert(OrderDetailForCreateDto OrderDetailForCreateDto)
        {
            var newData = _mapper.Map<OrderDetail>(OrderDetailForCreateDto);
            _repositoryManager.OrderDetailRepository.Insert(newData);
            _repositoryManager.Save();
        }

        public void Remove(OrderDetailDto OrderDetailDto)
        {
            var delete = _mapper.Map<OrderDetail>(OrderDetailDto);
            _repositoryManager.OrderDetailRepository.Remove(delete);
            _repositoryManager.Save();
        }
    }
}
