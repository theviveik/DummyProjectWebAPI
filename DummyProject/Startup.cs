﻿using BusinessAccessLayer;
using BusinessAccessLayer.Models;
using BusinessAccessLayer.Service;
using DataAccessLayer.DataModel;
using DataAccessLayer.Interface;
using DataAccessLayer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace DummyProject
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
            services.AddControllers();
            services.AddDbContext<DummyProjectContext>(options =>
            options.UseSqlServer(
                Configuration.GetSection("ConnectionStrings:name").Value,
                b => b.MigrationsAssembly(typeof(DummyProjectContext).Assembly.FullName)));

            //register automapper
            services.AddAutoMapper(Assembly.GetAssembly(typeof(ApplicationMapper)));

            //Inject Application Setting
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));

            services.AddCors();

            services.AddAuthenticationJwt(Configuration);

            services.DependencyInjections();

            services.SwaggerExtension();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ICustomLog logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dummy Project API V1");
                });
            }
            app.ErrorExceptionExtensions(logger);
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseCors(builder =>
            {
                builder.WithOrigins(Configuration.GetSection("ApplicationSettings:Client_URL").Value).AllowAnyMethod().AllowAnyHeader();
            });
        }
    }
}