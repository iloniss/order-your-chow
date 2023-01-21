using AutoMapper;
using FileProcessor.CORE.Services;
using FileProcessor.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.CORE.Contracts.Services;
using OrderYourChow.CORE.Services;
using OrderYourChow.CORE.Services.CRM.Product;
using OrderYourChow.CORE.Services.CRM.Recipe;
using OrderYourChow.CORE.Validators.CRM.Base;
using OrderYourChow.Repositories.Repositories.CRM.Product;
using OrderYourChow.Repositories.Repositories.CRM.Recipe;

var builder = WebApplication.CreateBuilder(args);
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterValidators();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "OrderYourChow",
        Version = "v1",
        Description = "Dodaj sk³adniki i przepisy, którch potrzebujesz.",
    });
});

//Services
builder.Services.AddScoped<IFileProcessor, FileProcessor.Services.FileProcessor>();
builder.Services.AddScoped<IFileProcessorValidator, FileProcessorValidator>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRecipeProductMeasureService, RecipeProductMeasureService>();
var config = new MapperConfiguration(cfg => cfg.AddMaps("OrderYourChow.Repositories"));

builder.Services.AddSingleton(s => config.CreateMapper());

//Repository
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IRecipeProductMeasureRepository, RecipeProductMeasureRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});

var configuration = new ConfigurationBuilder()
     .SetBasePath(Directory.GetCurrentDirectory())
     .AddEnvironmentVariables()
     .Build();

builder.Services.AddDbContext<OrderYourChow.DAL.CORE.Models.OrderYourChowContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("OrderYourChow"), b => b.MigrationsAssembly("OrderYourChow.DAL.CORE"));

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "OrderYourChow.CRM"));
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
ForwardedHeaders.XForwardedProto
});

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }