using AutoMapper;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.DAL.CORE.Models;

namespace OrderYourChow.Repositories.Mappings.CRM.Recipe
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<DRecipe, RecipeDTO>()
                .ForMember(d => d.RecipeId, opt => opt.MapFrom(src => src.RecipeId))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(d => d.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(d => d.RecipeCategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(d => d.Meat, opt => opt.MapFrom(src => src.Meat))
                .ForMember(d => d.MainImage, opt => opt.MapFrom(src => src.MainImage))
                .ReverseMap();

            CreateMap<RecipeCategoryDTO, SRecipeCategory>()
                .ForMember(d => d.RecipeCategoryId, opt => opt.MapFrom(src => src.RecipeCategoryId))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<RecipeProductMeasureDTO, SProductMeasure>()
                .ForMember(d => d.ProductMeasureId, opt => opt.MapFrom(src => src.ProductMeasureId))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<DRecipe, RecipeListDTO>()
                .ForMember(d => d.RecipeId, opt => opt.MapFrom(src => src.RecipeId))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Image, opt => opt.MapFrom(src => src.MainImage));

        }
    }
}
