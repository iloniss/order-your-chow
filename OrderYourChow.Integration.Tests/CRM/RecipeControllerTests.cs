using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.DAL.CORE.Models;

namespace OrderYourChow.Integration.Tests.CRM
{
    //TODO zmiana podawania parametrów
    [Collection("Integration")]
    public class RecipeControllerTests
         : IClassFixture<OrderYourChowCRMApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Program> _webAppFactory;
        private const string _url = "/api/Recipe";

        public RecipeControllerTests()
        {
            OrderYourChowCRMApplicationFactory<Program> webAppFactory = new();
            _httpClient = webAppFactory.CreateClient();
            _webAppFactory = webAppFactory;
        }

        [Fact]
        public async Task GetRecipes_ShouldReturnRecipes_WhenRecipesExists()
        {
            // Arrange
            Seed(null);

            // Act
            var response = await _httpClient.GetAsync(_url);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<RecipeListDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().NotBeEmpty();
            result.Should().HaveCount(2);
            result.Select(x => x.Name).Should().BeInAscendingOrder();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipes_ShouldReturnActiveRecipes_WhenActiveRecipesExists()
        {
            // Arrange
            Seed(null);

            // Act
            var response = await _httpClient.GetAsync(_url + "?isActive=" + true.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<RecipeListDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().NotBeEmpty();
            result.Should().HaveCount(1);

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipes_ShouldNotReturnActiveRecipes_WhenActiveRecipesNotExists()
        {
            // Arrange
            Seed(false);

            // Act
            var response = await _httpClient.GetAsync(_url + "?isActive=" + true.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<RecipeListDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().BeEmpty();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipes_ShouldReturnNotActiveRecipes_WhenNotActiveRecipesExists()
        {
            // Arrange
            Seed(null);

            // Act
            var response = await _httpClient.GetAsync(_url + "?isActive=" + false.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<RecipeListDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().NotBeEmpty();
            result.Should().HaveCount(1);

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipes_ShouldNotReturnNotActiveRecipes_WhenNotActiveRecipesNotExists()
        {
            // Arrange
            Seed(true);

            // Act
            var response = await _httpClient.GetAsync(_url + "?isActive=" + false.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<RecipeListDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().BeEmpty();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipes_ShouldNotReturnRecipes_WhenRecipesNotExists()
        {
            // Act
            var response = await _httpClient.GetAsync(_url);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<RecipeListDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().BeEmpty();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeCategories_ShouldReturnRecipeCategories_WhenRecipeCategoriesExist()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            context.SRecipeCategories.Add(new SRecipeCategory { Name = "dinner" });
            context.SRecipeCategories.Add(new SRecipeCategory { Name = "breakfast" });
            context.SRecipeCategories.Add(new SRecipeCategory { Name = "lunch" });
            context.SaveChanges();

            // Act
            var response = await _httpClient.GetAsync(_url + "/categories");

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<RecipeCategoryDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().NotBeEmpty();
            result.Should().HaveCount(3);
            result.Select(x => x.RecipeCategoryId).Should().BeInAscendingOrder();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeCategories_ShouldNotReturnRecipeCategories_WhenRecipeCategoriesNotExist()
        {
            // Arrange
            // Act
            var response = await _httpClient.GetAsync(_url + "/categories");

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<RecipeCategoryDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().BeEmpty();

            // Clean
            Clear();
        }

        protected void Clear()
        {
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            context.DRecipes.RemoveRange(context.DRecipes);
            context.SRecipeCategories.RemoveRange(context.SRecipeCategories);
            context.SaveChanges();
        }

        protected void Seed(bool? active = null)
        {
            string image = Guid.NewGuid().ToString();
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            var recipeCategory = new SRecipeCategory { Name = "dinner" };
            context.SRecipeCategories.Add(new SRecipeCategory { Name = "dinner" });
            context.DRecipes.Add(new DRecipe()
            {
                Category = recipeCategory,
                Active = active ?? true,
                MainImage = image,
                Name = "Scrambled eggs"
            });
            context.DRecipes.Add(new DRecipe()
            {
                Category = recipeCategory,
                Active = active ?? false,
                MainImage = image,
                Name = "Pancakes"
            });
            context.SaveChanges();
        }
    }
}
