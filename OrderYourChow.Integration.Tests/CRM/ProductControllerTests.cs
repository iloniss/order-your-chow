using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using System.IO;
using System.Net.Http.Headers;

namespace OrderYourChow.Integration.Tests.CRM
{
    public class ProductControllerTests
        : IClassFixture<OrderYourChowCRMApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Program> _webAppFactory;
        private const string _url = "/api/Product";
        private string _validImagePath = string.Concat(AppContext.BaseDirectory.AsSpan(0, AppContext.BaseDirectory.IndexOf("bin")), @"TestImages\", "69c42771-522b-4c33-b550-99685ec8b898.jpg");
        private string _invalidImagePath = string.Concat(AppContext.BaseDirectory.AsSpan(0, AppContext.BaseDirectory.IndexOf("bin")), @"TestImages\", "69c42771-522b-4c33-b550-99685ec8b898.txt");

        public ProductControllerTests()
        {
            OrderYourChowCRMApplicationFactory<Program> webAppFactory = new();
            _httpClient = webAppFactory.CreateClient();
            _webAppFactory = webAppFactory;
        }

        //Dlaczego nie zapisuje plików + porządek 
        [Fact]
        public async Task AddProduct_ShouldAddProduct_WhenProductNotExistsAndImageFileIsValid()
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

            MultipartFormDataContent content = new()
            {
                { new StringContent("Coffee"), "Name" },
                { new StringContent(productCategory.ProductCategoryId.ToString()), "ProductCategoryId" }
            };

            var fileInfo = new FileInfo(_validImagePath);
            var fileContent = new StreamContent(new FileStream(fileInfo.FullName, FileMode.Open));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "imageFile",
                FileName = fileInfo.Name
            };
            content.Add(fileContent);
            //Act
            var response = await _httpClient.PostAsync(_url, content);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.Created);

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProducts_ShouldReturnProducts_WhenProductsExists()
        {
            //Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductCategory productCategory = new()
            {
                Name = "Beverages"
            };
            context.SProductCategories.Add(productCategory);
            context.SProducts.Add(new SProduct()
            {
                Name = "Coffee",
                Category = productCategory
            });
            context.SProducts.Add(new SProduct()
            {
                Name = "Tea",
                Category = productCategory
            });
            context.SProducts.Add(new SProduct()
            {
                Name = "Beer",
                Category = productCategory
            });
            context.SaveChanges();

            //Act
            var response = await _httpClient.GetAsync(_url + "/list");

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<ProductDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().NotBeEmpty();
            result.Should().HaveCount(3);
            result.Select(x => x.Name).Should().BeInAscendingOrder();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProducts_ShouldNotReturnProduct_WhenProductsNotExist()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync(_url + "/list");

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<IList<ProductDTO>>(await response.Content.ReadAsStringAsync());
            result.Should().BeEmpty();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProduct_ShouldNotReturnProduct_WhenProductNotExists()
        {
            //Arrange
            //Act
            var response = await _httpClient.GetAsync(_url + "?productId=" + 3.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NoContent);
            var result = JsonConvert.DeserializeObject<ProductDTO>(await response.Content.ReadAsStringAsync());
            result.Should().BeNull();

            // Clean
            Clear();
        }

        [Fact]
        public async Task GetProduct_ShouldReturnProduct_WhenProductExists()
        {
            //Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductCategory productCategory = new()
            {
                Name = "Beverages"
            };
            context.SProductCategories.Add(productCategory);
            SProduct product = new()
            {
                Name = "Coffee",
                Category = productCategory
            };
            context.SProducts.Add(product);
            context.SaveChanges();

            //Act
            var response = await _httpClient.GetAsync(_url + "?productId=" + product.ProductId.ToString());

            //Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<ProductDTO>(await response.Content.ReadAsStringAsync());
            result.Should().NotBeNull();
            result.Name.Should().Be(product.Name);
            result.ProductCategory.Should().Be(productCategory.Name);
            result.ProductCategoryId.Should().Be(productCategory.ProductCategoryId);

            // Clean
            Clear();
        }

        protected void Clear()
        {
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            context.SProducts.RemoveRange(context.SProducts);
            context.SProductCategories.RemoveRange(context.SProductCategories);
            context.SaveChanges();
        }
    }
}
