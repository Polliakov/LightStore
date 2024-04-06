using LightStore.Application;
using LightStore.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LightStore.WebApi.Middleware;
using LightStore.Application.Mapping;
using LightStore.WebApi.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using LightStore.WebApi.Options;
using LightStore.ImageService;
using Microsoft.OpenApi.Models;

namespace LightStore.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddPersistance(Configuration);
            services.AddApplication();
            services.AddMappingConfiguration(
                    new ApplicationMappingRegister(),
                    new WebApiMappingRegister()
                );
            services.AddMappers();
            services.AddImageService(new ImageServiceOptions 
                { 
                    Url = Environment.IsDevelopment() ? 
                        "http://localhost:5002/images" : 
                        "http://api.artforge.polliakov.site/images",
                    SavePath = Environment.WebRootPath + "/images"
                });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                    policy.AllowAnyOrigin();
                });
            });

            services.Configure<AuthOptions>(Configuration.GetSection("Authentication"));

            var authOptions = Configuration.GetSection("Authentication").Get<AuthOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = authOptions.Issuer,
                            ValidAudience = authOptions.Audience,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                            LifetimeValidator = (_, expires, _, _) => 
                                expires > DateTime.UtcNow,
                        };
                    });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "LightStore", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(options => options.RouteTemplate = "swagger/{documentname}/swagger.json");
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "LightStore v1");
                });
                app.UseStaticFiles();
            }

            app.UseCustomExceptionHandler();

            app.UseRouting();

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
