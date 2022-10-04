using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.Product;
using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Services.Abstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProduct(bool trackChanges);

        Task<ProductDto> GetProductById(int productId, bool trackChanges);

        Task<ProductDto> GetProductPhotoOnSalesById(int productId, bool trackChanges);
        Task<ProductDto> GetProductOrderOnSalesById(int productId, bool trackChanges);

        Task<ProductPhotoGroupDto> GetProductPhotoGroupById(int productId, bool trackChanges);

        Task<IEnumerable<ProductDto>> GetProductOnSales(bool trackChanges);

        void CreateProductManyPhoto(ProductForCreateDto productForCreateDto, List<ProductPhotoCreateDto> productPhotoCreateDtos);

        void EditProductPhoto(ProductDto productDto, List<ProductPhotoDto> productPhotoDto);

        void Insert(ProductForCreateDto productForCreateDto);

        void Edit(ProductDto productDto);

        void Remove(ProductDto productDto);
    }
}
