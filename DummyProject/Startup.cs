using BusinessAccessLayer;
using BusinessAccessLayer.Models;
using BusinessAccessLayer.Service;
using System.Reflection;

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

            services.AddDbContexts(Configuration);

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
                app.UseSwaggerUIExtension();
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
