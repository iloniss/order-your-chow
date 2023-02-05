using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderYourChow.CORE.Contracts.API.Calendar;
using OrderYourChow.CORE.Contracts.API.Recipe;
using OrderYourChow.CORE.Contracts.API.User;
using OrderYourChow.CORE.Validators.API.Base;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Extensions;
using OrderYourChow.Repositories.Repositories.API.Calendar;
using OrderYourChow.Repositories.Repositories.API.Recipe;
using OrderYourChow.Repositories.Repositories.API.User;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterValidators();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "OrderYourChow",
        Version = "v1",
        Description = "Dodaj składniki i przepisy, którch potrzebujesz.",
    });
});

builder.Services.AddHealthChecks();

builder.Services.AddAppAutoMapper();

//Repository
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<ICalendarRepository, CalendarRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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

builder.Services.AddDbContext<OrderYourChowContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("OrderYourChow"), b => b.MigrationsAssembly("OrderYourChow.DAL.CORE"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "OrderYourChow"));
    app.UseDeveloperExceptionPage();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
    ForwardedHeaders.XForwardedProto
});

app.UseCors(MyAllowSpecificOrigins);

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{ }