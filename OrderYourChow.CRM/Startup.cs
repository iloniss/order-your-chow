using FileProcessor.CORE.Services;
using FileProcessor.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderYourChow.CORE.Contracts.CRM.Product;
using OrderYourChow.CORE.Contracts.CRM.Recipe;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.Repositories.Repositories.CRM.Recipe;
using OrderYourChow.Repositories.Repositories.CRM.Product;
using FluentValidation.AspNetCore;
using OrderYourChow.CORE.Validators.CRM.Base;
using Microsoft.AspNetCore.HttpOverrides;

namespace OrderYourChow.CRM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation();
            services.AddHealthChecks();


            services.AddDbContext<OrderYourChowContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    actions => actions.MigrationsAssembly("OrderYourChow.DAL.CORE"));
            });


            //Validators
            services.RegisterValidators();

            //Services
            services.AddScoped<IFileProcessor, FileProcessor.Services.FileProcessor>();
            services.AddScoped<IFileProcessorValidator, FileProcessorValidator>();

            services.AddAutoMapper(a => a.AddProfile<Repositories.Mappings.CRM.Recipe.RecipeProfile>(), typeof(Startup));
            services.AddAutoMapper(a => a.AddProfile<Repositories.Mappings.Shared.Recipe.RecipeProfile>(), typeof(Startup));
            services.AddAutoMapper(a => a.AddProfile<Repositories.Mappings.CRM.Product.ProductProfile>(), typeof(Startup));

            //Repository
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddScoped<IRecipeProductMeasureRepository, RecipeProductMeasureRepository>();


            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.SetIsOriginAllowed(isOriginAllowed: _ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });

            services.AddSwaggerGen(options =>
            {
                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "Co siê interesujesz",
                //    Name = "Authorization",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.Http,
                //    Scheme = "bearer",
                //});

                //var security = new OpenApiSecurityRequirement
                //{
                //    {
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference
                //            {
                //                Id = "Bearer",
                //                Type = ReferenceType.SecurityScheme
                //            },
                //            UnresolvedReference = true
                //        },
                //        new List<string>()
                //    }
                //};

                //options.AddSecurityRequirement(security);

                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "OrderYourChow",
                    Version = "v1",
                    Description = "Dodaj sk³adniki i przepisy, którch potrzebujesz.",
                });
                //options.OperationFilter<AddRequiredHeaderParameter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
            ForwardedHeaders.XForwardedProto
            });

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/84315CE7-EE19-4CC4-846C-68C35254C60D");
                endpoints.MapControllers();
            });


            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "OrderYourChow.CRM"));
        }
    }

}
