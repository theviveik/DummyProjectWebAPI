using DataAccessLayer.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessAccessLayer.Service
{
    public static class AddDataBaseContext
    {
        public static void AddDbContexts(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DummyProjectContext>(options =>
           options.UseSqlServer(
               Configuration.GetSection("ConnectionStrings:name").Value,
               b => b.MigrationsAssembly(typeof(DummyProjectContext).Assembly.FullName)));

        }
    }
}
