using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OrderYourChow.CORE.Models.CRM.Recipe;
using OrderYourChow.DAL.CORE.Models;

namespace OrderYourChow.Integration.Tests.CRM
{
    [Collection("Integration")]
    public class RecipeProductMeasureControllerTests
        : IClassFixture<OrderYourChowCRMApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Program> _webAppFactory;
        private const string _url = "/api/RecipeProductMeasure";

        public RecipeProductMeasureControllerTests()
        {
            OrderYourChowCRMApplicationFactory<Program> webAppFactory = new();
            _httpClient = webAppFactory.CreateClient();
            _webAppFactory = webAppFactory;
        }

        [Theory]
        [InlineData("kilogram", "kilogram")]
        [InlineData("kilogram", "liter")]
        public async Task UpdateRecipeProductMeasure_ShouldUpdateRecipeProductMeasure_WhenRecipeProductMeasureExitsAndIsValid(
            string oldName, string newName)
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductMeasure productMeasure = new()
            {
                Name = oldName
            };
            context.SProductMeasures.Add(productMeasure);
            context.SaveChanges();
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", newName),
                new KeyValuePair<string, string>("ProductMeasureId", productMeasure.ProductMeasureId.ToString())
            });

            //Act
            var response = await _httpClient.PutAsync(_url, formData);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NoContent);

            // Clean
            Clear();
        }

        [Fact]
        public async Task UpdateRecipeProductMeasure_ShouldNotUpdateRecipeProductMeasure_WhenRecipeProductMeasureNotExits()
        {
            // Arrange
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", "kilogram"),
                new KeyValuePair<string, string>("ProductMeasureId", 3.ToString())
            });

            //Act
            var response = await _httpClient.PutAsync(_url, formData);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Recipe.NotFoundRecipeProductMeasure);

            // Clean
            Clear();
        }

        [Fact]
        public async Task UpdateRecipeProductMeasure_ShouldNotUpdateRecipeProductMeasure_WhenRecipeProductMeasureNewNameExits()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductMeasure productMeasure = new()
            {
                Name = "liter"
            };
            context.SProductMeasures.Add(new SProductMeasure { Name = "kilogram" });
            context.SProductMeasures.Add(productMeasure);
            context.SaveChanges();
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", "kilogram"),
                new KeyValuePair<string, string>("ProductMeasureId", productMeasure.ProductMeasureId.ToString())
            });

            //Act
            var response = await _httpClient.PutAsync(_url, formData);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Recipe.ExistedRecipeProductMeasure);

            // Clean
            Clear();
        }

        [Fact]
        public async Task DeleteRecipeProductMeasure_ShouldDeleteRecipeProductMeasure_WhenRecipeProductMeasureExitsAndIsValid()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductMeasure productMeasure = new() { Name = "liter" };
            context.SProductMeasures.Add(productMeasure);
            context.SaveChanges();

            //Act
            var response = await _httpClient.DeleteAsync(_url + "/" + productMeasure.ProductMeasureId.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NoContent);

            // Clean
            Clear();
        }

        [Fact]
        public async Task DeleteRecipeProductMeasure_ShouldNotDeleteRecipeProductMeasure_WhenRecipeProductMeasureNotExits()
        {
            // Arrange
            //Act
            var response = await _httpClient.DeleteAsync(_url + "/" + 3.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Recipe.NotFoundRecipeProductMeasure);

            // Clean
            Clear();
        }

        [Fact]
        public async Task DeleteRecipeProductMeasure_ShouldNotDeleteRecipeProductMeasure_WhenRecipeProductMeasureIsUsed()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductMeasure productMeasure = new() { Name = "liter" };
            context.SProductMeasures.Add(productMeasure);
            SProductCategory productCategory = new() { Name = "Beverages" };
            context.SProductCategories.Add(productCategory);
            SProduct product = new() { Category = productCategory, Name = "Whisky" };
            context.SProducts.Add(product);
            SRecipeCategory recipeCategory = new() { Name = "Drink" };
            context.SRecipeCategories.Add(recipeCategory);
            DRecipe recipe = new() { Name = "Whiskey sour", Category = recipeCategory };
            context.DRecipes.Add(recipe);
            DRecipeProduct recipeProduct = new() { Recipe = recipe, Product = product, Count = 1, ProductMeasure = productMeasure };
            context.DRecipeProducts.Add(recipeProduct);
            context.SaveChanges();

            //Act
            var response = await _httpClient.DeleteAsync(_url + "/" + productMeasure.ProductMeasureId.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Recipe.UsedRecipeProductMeasure);

            //Clean
            context.RemoveRange(context.DRecipeProducts);
            context.RemoveRange(context.DRecipes);
            context.RemoveRange(context.SRecipeCategories);
            context.RemoveRange(context.SProducts);
            context.RemoveRange(context.SProductCategories);
            context.SaveChanges();
            Clear();
        }

        [Fact]
        public async Task AddRecipeProductMeasure_ShouldNotAddRecipeProductMeasure_WhenRecipeProductMeasureExists()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            context.SProductMeasures.Add(new SProductMeasure { Name = "kilogram" });
            context.SaveChanges();
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", "kilogram")
            });

            //Act
            var response = await _httpClient.PostAsync(_url, formData);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Recipe.ExistedRecipeProductMeasure);

            // Clean
            Clear();
        }

        [Fact]
        public async Task AddRecipeProductMeasure_ShouldAddRecipeProductMeasure_WhenRecipeProductMeasureNotExists()
        {
            // Arrange
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", "kilogram")
            });

            //Act
            var response = await _httpClient.PostAsync(_url, formData);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.Created);

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeProductMeasures_ShouldReturnRecipeProductMeasures_WhenRecipeProductMeasuresExist()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            context.SProductMeasures.Add(new SProductMeasure { Name = "kilogram" });
            context.SProductMeasures.Add(new SProductMeasure { Name = "gram" });
            context.SProductMeasures.Add(new SProductMeasure { Name = "liter" });
            context.SaveChanges();

            // Act
            var response = await _httpClient.GetAsync(_url);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<RecipeProductMeasureDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().NotBeEmpty();
            result.Should().HaveCount(3);
            result.Select(x => x.Name).Should().BeInAscendingOrder();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeProductMeasures_ShouldNotReturnRecipeProductMeasures_WhenRecipeProductMeasuresNotExist()
        {
            // Arrange
            // Act
            var response = await _httpClient.GetAsync(_url);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<RecipeProductMeasureDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().BeEmpty();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeProductMeasure_ShouldNotReturnRecipeProductMeasure_WhenRecipeProductMeasureNotExits()
        {
            // Arrange
            // Act
            var response = await _httpClient.GetAsync(_url + "/" + 3.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NoContent);
            var result = JsonConvert.DeserializeObject<RecipeProductMeasureDTO>(await response.Content.ReadAsStringAsync());
            result.Should().BeNull();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetRecipeProductMeasure_ShouldReturnRecipeProductMeasure_WhenRecipeProductMeasureExits()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductMeasure productMeasure = new()
            {
                Name = "kilogram"
            };
            context.SProductMeasures.Add(productMeasure);
            context.SaveChanges();

            // Act
            var response = await _httpClient.GetAsync(_url + "/" + productMeasure.ProductMeasureId.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<RecipeProductMeasureDTO>(await response.Content.ReadAsStringAsync());
            result.Should().NotBeNull();
            result.Name.Should().Be(productMeasure.Name);

            // Clean
            Clear();
        }

        protected void Clear()
        {
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            context.SProductMeasures.RemoveRange(context.SProductMeasures);
            context.SaveChanges();
        }
    }
}
