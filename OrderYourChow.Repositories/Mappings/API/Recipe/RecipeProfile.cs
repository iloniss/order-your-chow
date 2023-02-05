using AutoMapper;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.CORE.Models.API.Recipe;

namespace OrderYourChow.Repositories.Mappings.API.Recipe
{
    public class RecipeProfile : Profile
    {
        public RecipeProfile()
        {
            CreateMap<DRecipe, RecipeInfoDTO>()
                .ForMember(d => d.RecipeId, opt => opt.MapFrom(src => src.RecipeId))
                .ForMember(d => d.RecipeName, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(d => d.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(d => d.Favourite, opt => opt.MapFrom(src => src.Favourite))
                .ForMember(d => d.MainImage, opt => opt.MapFrom(src => src.MainImage))
                .ForMember(d => d.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<DDietDayRecipe, RecipeDTO>()
                .ForMember(d => d.RecipeId, opt => opt.MapFrom(src => src.RecipeId))
                .ForMember(d => d.RecipeName, opt => opt.MapFrom(src => src.DRecipe.Name))
                .ForMember(d => d.Duration, opt => opt.MapFrom(src => src.DRecipe.Duration))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.DRecipe.Description))
                .ForMember(d => d.MainImage, opt => opt.MapFrom(src => src.DRecipe.MainImage))
                .ForMember(d => d.RecipeProducts, opt => opt.MapFrom(src => src.DRecipe.DRecipeProducts))
                .ForMember(d => d.Eaten, opt => opt.MapFrom(src => src.Eaten))
                .ForMember(d => d.Multiplier, opt => opt.MapFrom(src => src.Multiplier));

            CreateMap<DDietDayRecipe, RecipeDayListDTO>()
                .ForMember(d => d.DietDayRecipeId, opt => opt.MapFrom(src => src.DietDayRecipeId))
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(src => src.DRecipe.Category.Name));

            CreateMap<DRecipe, RecipeExchangeDTO>()
                .ForMember(d => d.RecipeId, opt => opt.MapFrom(src => src.RecipeId))
                .ForMember(d => d.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(d => d.RecipeName, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Rating, opt => opt.MapFrom(src => src.Rating))
                .ForMember(d => d.Favourite, opt => opt.MapFrom(src => src.Favourite));
            
        }
    }
} 
