using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.Framework;
using AppointmentRx.Models.Validators.CustomErrorConfiguration;
using AppointmentRx.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

namespace AppointmentRx.WebApi.DependencyInjection
{
    public static class DependencyServices
    {
        private readonly static ILoggerFactory _loggerFactory;
        public static void Inject(IServiceCollection services, IConfiguration config)
        {
            //var otpConfig = config.GetSection("OtpConfiguration").Get<OtpConfiguration>();
            //services.AddSingleton(otpConfig);
            services.AddControllers();

            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<PortalDbContext>(x =>
            {
                x.UseSqlServer(connectionString);
                x.UseLoggerFactory(_loggerFactory);
            });

            //services.AddDbContext<SecurityDbContext>(x =>
            //{
            //    x.UseSqlServer(connectionString);
            //    x.UseLoggerFactory(_loggerFactory);
            //});
            //services.AddIdentity<UserCredential, IdentityRole>(x =>
            //{
            //    x.Password.RequiredLength = 4;
            //    x.Password.RequireNonAlphanumeric = false;
            //    x.Password.RequireUppercase = false;
            //    x.Password.RequireLowercase = false;
            //    x.Password.RequireDigit = false;
            //}).AddEntityFrameworkStores<SecurityDbContext>().AddDefaultTokenProviders();

            //Services
            services.AddScoped<ICipherService, CipherService>();
            services.AddScoped<ICommonService, CommonService>();
            //services.AddScoped<ITokenService, TokenService>();
            //Repository

            //Business

            //Fluent Validation Custom Error Model Interceptor
            services.AddTransient<IValidatorInterceptor, CustomErrorModelInterceptor>();
        }
    }
}
