using AutoMapper;
using OrderYourChow.CORE.Models.Shared.Recipe;
using OrderYourChow.DAL.CORE.Models;

namespace OrderYourChow.Repositories.Mappings.Shared.Recipe
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<RecipeProductDTO, DRecipeProduct>()
                .ForMember(d => d.RecipeProductId, opt => opt.MapFrom(src => src.RecipeProductId))
                .ForMember(d => d.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(d => d.ProductMeasureId, opt => opt.MapFrom(src => src.ProductMeasureId))
                .ForMember(d => d.Count, opt => opt.MapFrom(src => src.Count))
                .ReverseMap();
        }
    }
}
