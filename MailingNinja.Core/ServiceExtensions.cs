using System.Reflection;
using DinkToPdf;
using DinkToPdf.Contracts;
using MailingNinja.Contracts.Services;
using MailingNinja.Core.Mappers;
using MailingNinja.Core.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MailingNinja.Core
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            // PDF Service
            services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));
            services.AddScoped<IPdfService, PdfService>();
            
            // Template Service
            services.AddScoped<ITemplateService, TemplateService>();

            // Mailing Service
            services.AddSingleton<IMailingService, MailingService>();

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(x => x.AddProfile<NinjaMapperProfile>());
            return services;
        }
    }
}