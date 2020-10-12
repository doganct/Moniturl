using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moniturl.Core;
using Moniturl.Data;

namespace Moniturl.Service
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSeviceExtensions(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbSettings:ConnectionString"];

            services.AddDbContext<MoniturlDbContext>(opts =>
            {
                opts.UseSqlServer(connectionString);
            });

            services.AddAutoMapper(typeof(MappingProfiles));

            services.Configure<SmtpSettings>(configuration.GetSection(nameof(SmtpSettings)));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITargetService, TargetService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITargetLogService, TargetLogService>();
            services.AddScoped<IMailService, MailService>();


        }
    }
}
