using Microsoft.Extensions.DependencyInjection;

using ESDAVDataAccess.Infraestructure;
using Microsoft.EntityFrameworkCore;
using ESDAVDomain.IRepositories;
using ESDAVDataAccess.Infraestructure.RepositoryEntities.Repository;
using AgroCampo_Business.Services.Interface;
using AgroCampo_Business.Services.Implementation;
using AgroCampo_Business.MailService;
using System.Collections.Generic;

namespace ESDAVBusiness
{
    public static class DependencyInjection
    {


        public static IServiceCollection AddDbContextBusiness(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<DataBaseContext>(
                options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("AgroCampo_API")));
            return services;
        }


        public static IServiceCollection AddInfrastructureBusiness(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            #region Repositorios

            services.AddTransient<IBasicasRepository, BasicasRepository>();

            services.AddTransient<IExampleRepository, ExampleRepository>();

            #endregion
            #region Servicios

            services.AddTransient<IMailSender, CustomEmailSender>();

            services.AddTransient<IExampleService, ExampleService>();


            #endregion

            #region httpClientServices
            #endregion

            #region BackgroundServices
            //services.AddHostedService<EmailJobService>();
            #endregion

            return services;
        }
    }
}
