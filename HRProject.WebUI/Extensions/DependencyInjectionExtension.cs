using HRProject.Business.Abstract;
using HRProject.Business.Concrete;
using HRProject.DataAccess.Abstract;
using HRProject.DataAccess.Concrete.EntityFramework;
using System.Reflection;

namespace HRProject.WebUI.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddServiceInjections(this IServiceCollection services)
        {
            
            services.AddScoped<IPersonelBLL, PersonelBLL>();
            services.AddScoped<IPersonelRepository, EfPersonelRepository>();
            services.AddScoped<IAllowanceRepository,EfAllowanceRepository>();
            services.AddScoped<IAllowanceBLL, AllowanceBLL>();
            services.AddScoped<IExpenseRepository, EfExpanseRepository>();
            services.AddScoped<IExpenseBLL, ExpenseBLL>();
            services.AddScoped<IManagerBLL, ManagerBLL>();
            return services;
        }
    }
}
