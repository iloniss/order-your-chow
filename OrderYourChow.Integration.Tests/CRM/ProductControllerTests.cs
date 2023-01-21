using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OrderYourChow.CORE.Models.CRM.Product;
using OrderYourChow.DAL.CORE.Models;
using System.Net.Http.Headers;
using Directory = OrderYourChow.Integration.Tests.Data.Directory;

namespace OrderYourChow.Integration.Tests.CRM
{
    [Collection("Integration")]
    public class ProductControllerTests
        : IClassFixture<OrderYourChowCRMApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Program> _webAppFactory;
        private const string _url = "/api/Product";
        private static readonly string _directory = Directory.GetDirectory();
        private static readonly string _validImagePath = Path.Combine(_directory, @"TestImages\69c42771-522b-4c33-b550-99685ec8b898.jpg");
        private static readonly string _invalidImagePath = Path.Combine(_directory, @"TestImages\69c42771-522b-4c33-b550-99685ec8b898.txt");

        public ProductControllerTests()
        {
            OrderYourChowCRMApplicationFactory<Program> webAppFactory = new();
            _httpClient = webAppFactory.CreateClient();
            _webAppFactory = webAppFactory;
            Directory.CreateTestDirectory();
        }


        [Theory]
        [InlineData("Coffee", "Coffee", true)]
        [InlineData("Coffee", "Tea", true)]
        [InlineData("Coffee", "Coffee", false)]
        [InlineData("Coffee", "Tea", false)]
        public async Task UpdateProduct_ShouldUpdateProduct_WhenProductExistsAndIsValid(
            string oldName, string newName, bool fileUpdated)
        {
            FileStream fs = null;
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductCategory productCategory = new()
            {
                Name = "Beverages"
            };
            context.SProductCategories.Add(productCategory);
            SProduct product = new() { Category = productCategory, Name = oldName, Image = "69c42771-522b-4c33-b550-99685ec8b898.jpg" };
            context.SProducts.Add(product);
            context.SaveChanges();

            MultipartFormDataContent content = new()
            {
                { new StringContent(newName), "Name" },
                { new StringContent(product.ProductId.ToString()), "ProductId" },
                { new StringContent(productCategory.ProductCategoryId.ToString()), "ProductCategoryId" }
            };


            if (fileUpdated)
            {
                File.Copy(_directory + @"\TestImages\69c42771-522b-4c33-b550-99685ec8b898.jpg", _directory + @"\wwwroot\images\products\69c42771-522b-4c33-b550-99685ec8b898.jpg");

                var fileInfo = new FileInfo(_validImagePath);
                //Tu jest problem
                fs = new FileStream(fileInfo.FullName, FileMode.Open);
                var fileContent = new StreamContent(fs);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "imageFile",
                    FileName = fileInfo.Name
                };
                content.Add(fileContent);
            }

            //Act
            var response = await _httpClient.PutAsync(_url, content);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NoContent);

            // Clean
            fs?.Dispose();
            ClearFiles();
            Clear();          
        }

        [Fact]
        public async Task UpdateProduct_ShouldNotUpdateProduct_WhenProductNotExists()
        {
            // Arrange
            MultipartFormDataContent content = new()
            {
                { new StringContent("Coffee"), "Name" },
                { new StringContent(3.ToString()), "ProductId" },
                { new StringContent(3.ToString()), "ProductCategoryId" }
            };

            //Act
            var response = await _httpClient.PutAsync(_url, content);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Product.NotFoundProduct);

            // Clean
            Clear();
        }

        [Fact]
        public async Task UpdateProduct_ShouldNotUpdateProduct_WhenImageIsInvalid()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductCategory productCategory = new()
            {
                Name = "Beverages"
            };
            context.SProductCategories.Add(productCategory);
            SProduct product = new() { Category = productCategory, Name = "Coffee" };
            context.SProducts.Add(product);
            context.SaveChanges();

            MultipartFormDataContent content = new()
            {
                { new StringContent("Coffee"), "Name" },
                { new StringContent(product.ProductId.ToString()), "ProductId" },
                { new StringContent(productCategory.ProductCategoryId.ToString()), "ProductCategoryId" }
            };

            var fileInfo = new FileInfo(_invalidImagePath);
            var fileContent = new StreamContent(new FileStream(fileInfo.FullName, FileMode.Open));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "imageFile",
                FileName = fileInfo.Name
            };
            content.Add(fileContent);

            //Act
            var response = await _httpClient.PutAsync(_url, content);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.Shared.Global.InvalidFile);

            // Clean
            Clear();
        }

        [Fact]
        public async Task UpdateProduct_ShouldNotUpdateProduct_WhenProductNewNameExits()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductCategory productCategory = new()
            {
                Name = "Beverages"
            };
            context.SProductCategories.Add(productCategory);
            SProduct product = new() { Category = productCategory, Name = "Coffee" };
            context.SProducts.Add(product);
            context.SaveChanges();

            MultipartFormDataContent content = new()
            {
                { new StringContent("Coffee"), "Name" },
                { new StringContent((product.ProductId + 1).ToString()), "ProductId" },
                { new StringContent(productCategory.ProductCategoryId.ToString()), "ProductCategoryId" }
            };

            //Act
            var response = await _httpClient.PutAsync(_url, content);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Product.ExistedProduct);

            // Clean
            Clear();
        }

        [Fact]
        public async Task DeleteProduct_ShouldDeleteProduct_WhenProductExitsAndIsValid()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductCategory productCategory = new() { Name = "Beverages" };
            context.SProductCategories.Add(productCategory);
            SProduct product = new() { Category = productCategory, Name = "Whisky", Image = "69c42771-522b-4c33-b550-99685ec8b898.jpg" };
            context.SProducts.Add(product);
            context.SaveChanges();

            File.Copy(_directory + @"\TestImages\69c42771-522b-4c33-b550-99685ec8b898.jpg", _directory + @"\wwwroot\images\products\69c42771-522b-4c33-b550-99685ec8b898.jpg");

            //Act
            var response = await _httpClient.DeleteAsync(_url + "/" + product.ProductId.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NoContent);

            // Clean
            Clear();
        }

        [Fact]
        public async Task DeleteProduct_ShouldNotDeleteProduct_WhenProductIsUsed()
        {
            // Arrange

            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductCategory productCategory = new() { Name = "Beverages" };
            context.SProductCategories.Add(productCategory);
            SProduct product = new() { Category = productCategory, Name = "Whisky", Image = "69c42771-522b-4c33-b550-99685ec8b898.jpg" };
            context.SProducts.Add(product);
            SRecipeCategory recipeCategory = new() { Name = "Drink" };
            context.SRecipeCategories.Add(recipeCategory);
            DRecipe recipe = new() { Name = "Whiskey sour", Category = recipeCategory };
            context.DRecipes.Add(recipe);
            SProductMeasure productMeasure = new() { Name = "Glass" };
            DRecipeProduct recipeProduct = new() { Recipe = recipe, Product = product, Count = 1, ProductMeasure = productMeasure };
            context.DRecipeProducts.Add(recipeProduct);
            context.SaveChanges();

            File.Copy(_directory + @"\TestImages\69c42771-522b-4c33-b550-99685ec8b898.jpg", _directory + @"\wwwroot\images\products\69c42771-522b-4c33-b550-99685ec8b898.jpg");

            //Act
            var response = await _httpClient.DeleteAsync(_url + "/" + product.ProductId.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Product.UsedProduct);

            // Clean
            context.RemoveRange(context.DRecipeProducts);
            context.RemoveRange(context.DRecipes);
            context.RemoveRange(context.SRecipeCategories);
            context.SaveChanges();
            ClearFiles();
            Clear();
        }

        [Fact]
        public async Task DeleteProduct_ShouldNotDeleteProduct_WhenProductNotExits()
        {
            // Arrange
            //Act
            var response = await _httpClient.DeleteAsync(_url + "/" + 3.ToString());

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.NotFound);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Product.NotFoundProduct);

            // Clean
            Clear();
        }

        [Fact]
        public async Task AddProduct_ShouldNotAddProduct_WhenImageFileIsInvalid()
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

            var fileInfo = new FileInfo(_invalidImagePath);
            var fileContent = new StreamContent(new FileStream(fileInfo.FullName, FileMode.Open));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "imageFile",
                FileName = fileInfo.Name
            };
            content.Add(fileContent);

            //Act
            var response = await _httpClient.PostAsync(_url, content);

            // Assert
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.Shared.Global.InvalidFile);

            // Clean
            Clear();
        }

        [Fact]
        public async Task AddProduct_ShouldNotAddProduct_WhenProductExists()
        {
            // Arrange
            using var scope = _webAppFactory.Services.CreateScope();
            OrderYourChowContext context = scope.ServiceProvider.GetRequiredService<OrderYourChowContext>();
            SProductCategory productCategory = new()
            {
                Name = "Beverages"
            };
            context.SProductCategories.Add(productCategory);
            context.SProducts.Add(new SProduct() { Category = productCategory, Name = "Coffee" });
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
            response.Should().HaveStatusCode(System.Net.HttpStatusCode.BadRequest);
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());
            result["message"].Should().Be(CORE.Const.CRM.Product.ExistedProduct);

            // Clean
            Clear();
        }

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
            ClearFiles();
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

        protected static void ClearFiles()
        {
            List<string> files = new(System.IO.Directory.GetFiles(Path.Combine(_directory, @"wwwroot/images/products")));
            files.ForEach(x => File.Delete(x));
        }
    }
}