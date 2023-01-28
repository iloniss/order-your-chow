using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.DAL.CORE.Models;

namespace OrderYourChow.Integration.Tests.CRM
{
    [Collection("Integration")]
    public class ProductCategoryControllerTests 
        : IClassFixture<OrderYourChowCRMApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Program> _webAppFactory;
        private const string _url = "/api/ProductCategory";
        public ProductCategoryControllerTests()
        {
            OrderYourChowCRMApplicationFactory<Program> webAppFactory = new();
            _httpClient = webAppFactory.CreateClient();
            _webAppFactory = webAppFactory;
        }

        [Theory]
        [InlineData("Beverages", "Beverages")]
        [InlineData("Beverages", "Dairy")]
        public async Task UpdateProductCategory_ShouldUpdateProductCategory_WhenProductCategoryExitsAndIsValid(
            string oldName, string newName)
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductCategory productCategory = new()
            {
                Name = oldName
            };
            context.SProductCategories.Add(productCategory);
            context.SaveChanges();
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", newName),
                new KeyValuePair<string, string>("ProductCategoryId", productCategory.ProductCategoryId.ToString())
            });

            //Act
            var response = await _httpClient.PutAsync(_url, formData);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NoContent);

            // Clean
            Clear();
        }

        [Fact]
        public async Task UpdateProductCategory_ShouldNotUpdateProductCategory_WhenProductCategoryNotExits()
        {
            // Arrange
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", "Beverages"),
                new KeyValuePair<string, string>("ProductCategoryId", 3.ToString())
            });

            //Act
            var response = await _httpClient.PutAsync(_url, formData);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Product.NotFoundProductCategory);

            // Clean
            Clear();
        }

        [Fact]
        public async Task UpdateProductCategory_ShouldNotUpdateProductCategory_WhenProductCategoryNewNameExits()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductCategory productCategory = new()
            {
                Name = "Entrees"
            };
            context.SProductCategories.Add(new SProductCategory { Name = "Beverages" });
            context.SProductCategories.Add(productCategory);
            context.SaveChanges();
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", "Beverages"),
                new KeyValuePair<string, string>("ProductCategoryId", productCategory.ProductCategoryId.ToString())
            });

            //Act
            var response = await _httpClient.PutAsync(_url, formData);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Product.ExistedProductCategory);

            // Clean
            Clear();
        }

        [Fact]
        public async Task DeleteProductCategory_ShouldDeleteProductCategory_WhenProductCategoryExitsAndIsValid()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductCategory productCategory = new()
            {
                Name = "Beverages"
            };
            context.SProductCategories.Add(productCategory);
            context.SaveChanges();

            //Act
            var response = await _httpClient.DeleteAsync(_url + "/" + productCategory.ProductCategoryId.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NoContent);

            // Clean
            Clear();
        }

        [Fact]
        public async Task DeleteProductCategory_ShouldNotDeleteProductCategory_WhenProductCategoryNotExits()
        {
            // Arrange
            //Act
            var response = await _httpClient.DeleteAsync(_url + "/" + 3.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Product.NotFoundProductCategory);

            // Clean
            Clear();
        }

        [Fact]
        public async Task DeleteProductCategory_ShouldNotDeleteProductCategory_WhenProductCategoryIsUsed()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductCategory productCategory = new()
            {
                Name = "Beverages"
            };
            context.SProductCategories.Add(productCategory);
            context.SProducts.Add(new SProduct { Category = productCategory, Name = "Cola" });
            context.SaveChanges();

            //Act
            var response = await _httpClient.DeleteAsync(_url + "/" + productCategory.ProductCategoryId.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Product.UsedProductCategory);

            //Clean
            context.Remove(context.SProducts.SingleOrDefault());
            context.SaveChanges();
            Clear();
        }

        [Fact]
        public async Task AddProductCategory_ShouldNotAddProductCategory_WhenProductCategoryExists()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            context.SProductCategories.Add(new SProductCategory { Name = "Beverages" });
            context.SaveChanges();
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", "Beverages")
            });

            //Act
            var response = await _httpClient.PostAsync(_url, formData);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Product.ExistedProductCategory);

            // Clean
            Clear();
        }

        [Fact]
        public async Task AddProductCategory_ShouldAddProductCategory_WhenProductCategoryNotExists()
        {
            // Arrange
            var formData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Name", "Beverages")
            });

            //Act
            var response = await _httpClient.PostAsync(_url, formData);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.Created);

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProductCategories_ShouldReturnProductCategories_WhenProductCategoriesExist()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            context.SProductCategories.Add(new SProductCategory { Name = "Beverages" });
            context.SProductCategories.Add(new SProductCategory { Name = "Entrees" });
            context.SProductCategories.Add(new SProductCategory { Name = "Dairy" });
            context.SaveChanges();

            // Act
            var response = await _httpClient.GetAsync(_url);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<ProductCategoryDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().NotBeEmpty();
            result.Should().HaveCount(3);
            result.Select(x => x.Name).Should().BeInAscendingOrder();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProductCategories_ShouldNotReturnProductCategories_WhenProductCategoriesNotExist()
        {
            // Arrange
            // Act
            var response = await _httpClient.GetAsync(_url);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<ProductCategoryDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().BeEmpty();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProductCategory_ShouldNotReturnProductCategory_WhenProductCategoryNotExits()
        {
            // Arrange
            // Act
            var response = await _httpClient.GetAsync(_url + "/" + 3.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NoContent);
            var result = JsonConvert.DeserializeObject<ProductCategoryDTO>(await response.Content.ReadAsStringAsync());
            result.Should().BeNull();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProductCategory_ShouldReturnProductCategory_WhenProductCategoryExits()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductCategory productCategory = new()
            {
                Name = "Beverages"
            };
            context.SProductCategories.Add(productCategory);
            context.SaveChanges();

            // Act
            var response = await _httpClient.GetAsync(_url + "/" + productCategory.ProductCategoryId.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<ProductCategoryDTO>(await response.Content.ReadAsStringAsync());
            result.Should().NotBeNull();
            result.Name.Should().Be(productCategory.Name);

            // Clean
            Clear();
        }

        protected void Clear()
        {
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            context.SProductCategories.RemoveRange(context.SProductCategories);
            context.SaveChanges();
        }
    }
}
