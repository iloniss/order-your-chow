using AutoMapper;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.CORE.Models.CRM.Product;

namespace OrderYourChow.Repositories.Mappings.CRM.Product
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductCategoryDTO, SProductCategory>()
                .ForMember(d => d.ProductCategoryId, opt => opt.MapFrom(src => src.ProductCategoryId))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<SProduct, ProductDTO>()
                .ForMember(d => d.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.ProductCategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(d => d.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(d => d.ProductCategory, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();

            CreateMap<AddProductDTO, SProduct>()
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.CategoryId, opt => opt.MapFrom(src => src.ProductCategoryId))
                .ForMember(d => d.Image, opt => opt.MapFrom(src => src.Image))
                .ReverseMap();
        }
    }
}
