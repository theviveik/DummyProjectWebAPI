using DataAccessLayer.Interface;
using DataAccessLayer.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessAccessLayer.Service
{
    public static class DependencyInjection
    {
        public static void DependencyInjections(this IServiceCollection services)
        {
            services.AddSingleton<ICustomLog, CustomLog>();
            #region Repositories
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
