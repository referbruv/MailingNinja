using MailingNinja.Contracts.Data;
using MailingNinja.Contracts.Data.Repositories;
using MailingNinja.Infrastructure.Data;
using MailingNinja.Infrastructure.Data.Context;
using MailingNinja.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MailingNinja.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSqlite<DatabaseContext>("Data Source=app.db;");

            services.AddScoped<INinjasRepository, NinjasRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            return services;
        }
    }
}