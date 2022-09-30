using AutoMapper;
using Northwind.Contracts.Dto.Category;
using Northwind.Contracts.Dto.Product;
using Northwind.Contracts.Dto.Supplier;
using Northwind.Domain.Models;

namespace Northwind.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryForCreateDto>().ReverseMap();

            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Supplier, SupplierForCreateDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductForCreateDto>().ReverseMap();

            CreateMap<ProductPhoto, ProductPhotoDto>().ReverseMap();
            CreateMap<ProductPhoto, ProductPhotoCreateDto>().ReverseMap();

            CreateMap<ProductPhoto, ProductPhotoGroupDto>()
                .ForPath(p => p.productDto.ProductName, pp => pp.MapFrom(p => p.PhotoProduct.ProductName))
                .ForPath(p => p.productDto.Supplier.CompanyName, pp => pp.MapFrom(p => p.PhotoProduct.Supplier.CompanyName))
                .ForPath(p => p.productDto.Category.CategoryName, pp => pp.MapFrom(p => p.PhotoProduct.Category.CategoryName))
                .ForPath(p => p.productDto.QuantityPerUnit, pp => pp.MapFrom(p => p.PhotoProduct.QuantityPerUnit))
                .ForPath(p => p.productDto.UnitPrice, pp => pp.MapFrom(p => p.PhotoProduct.UnitPrice))
                .ForPath(p => p.productDto.UnitsInStock, pp => pp.MapFrom(p => p.PhotoProduct.UnitsInStock))
                .ForPath(p => p.productDto.Discontinued, pp => pp.MapFrom(p => p.PhotoProduct.Discontinued))

                .ReverseMap();
        }
    }
}
