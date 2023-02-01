using FileProcessor.CORE.Services;
using FileProcessor.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using OrderYourChow.CORE.Validators.CRM.Base;
using OrderYourChow.CRM.Extensions;
using OrderYourChow.Infrastructure.Services;

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

builder.Services.AddScoped<IFileProcessor, FileProcessor.Services.FileProcessor>();
builder.Services.AddScoped<IFileProcessorValidator, FileProcessorValidator>();
builder.Services.Scan(
    x =>
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("FileProcessor") || x.FullName.Contains("OrderYourChow")).ToList();
        x.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo<IScoped>())
                .AsImplementedInterfaces()
                .WithScopedLifetime();
    });

builder.Services.AddAppAutoMapper();


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

app.UseStaticFiles();

app.UseCors(MyAllowSpecificOrigins);

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }