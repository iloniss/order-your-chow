using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderYourChow.DAL.CORE.Models;
using OrderYourChow.CORE.Contracts.API.Calendar;
using OrderYourChow.CORE.Contracts.API.Recipe;
using OrderYourChow.Repositories.Repositories.API.Calendar;
using OrderYourChow.Repositories.Repositories.API.Recipe;
using OrderYourChow.CORE.Mappings.API.Calendar;
using OrderYourChow.Repositories.Repositories.API.User;
using OrderYourChow.CORE.Contracts.API.User;
using OrderYourChow.Repositories.Mappings.API.User;
using OrderYourChow.CORE.Validators.API.Base;

namespace OrderYourChow
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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

            services.AddAutoMapper(a => a.AddProfile<Repositories.Mappings.API.Recipe.RecipeProfile>(), typeof(Startup));
            services.AddAutoMapper(a => a.AddProfile<Repositories.Mappings.Shared.Recipe.RecipeProfile>(), typeof(Startup));
            services.AddAutoMapper(a => a.AddProfile<CalendarProfile>(), typeof(Startup));
            services.AddAutoMapper(a => a.AddProfile<UserProfile>(), typeof(Startup));

            //Repository
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<ICalendarRepository, CalendarRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Validators
            services.RegisterValidators();


            services.AddSwaggerGen(options =>
            {
                //options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "Co się interesujesz",
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
                    Description = "Zaplanuj swój indywidualny jadłospis.",
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/19584477-8AEC-4B8B-B95A-56A4BBF57A06");
                endpoints.MapControllers();
            });


            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "OrderYourChow"));
        }

    }
}
