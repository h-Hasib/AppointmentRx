
using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Doctor.Chamber;
using AppointmentRx.DataAccess.Repositories.Doctor.Profile;
using AppointmentRx.DataAccess.Repositories.Patient.Profile;
using AppointmentRx.Framework;
using AppointmentRx.Models.Validators.CustomErrorConfiguration;
using AppointmentRx.Services;
using AppointmentRx.WebApi.Tokens;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppointmentRx.WebApi.DependencyInjection
{
    public static class DependencyServices
    {
        private readonly static ILoggerFactory _loggerFactory;
        public static void Inject(IServiceCollection services, IConfiguration config)
        {
            


            var connectionString = config.GetConnectionString("DefaultConnection");
            services.AddDbContext<PortalDbContext>(x =>
            {
                x.UseSqlServer(connectionString);
                x.UseLoggerFactory(_loggerFactory);
            });

            //services.AddIdentity<PortalUser, IdentityRole>();
            //services.AddIdentity<PortalUser, IdentityRole>(x =>
            //{
            //    x.Password.RequiredLength = 4;
            //    x.Password.RequireNonAlphanumeric = false;
            //    x.Password.RequireUppercase = false;
            //    x.Password.RequireLowercase = false;
            //    x.Password.RequireDigit = false;
            //}).AddEntityFrameworkStores<SecurityDbContext>().AddDefaultTokenProviders();
            services.AddControllers();
            services.AddIdentity<PortalUser, IdentityRole>()
                        .AddEntityFrameworkStores<PortalDbContext>()
                        .AddDefaultTokenProviders();


            //Services
            services.AddScoped<ICipherService, CipherService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<ITokenService, TokenService>();
            //Repository
            services.AddTransient<IPatientProfileRepository, PatientProfileRepository>();
            services.AddTransient<IDoctorProfileRepository, DoctorProfileRepository>();
            services.AddTransient<IChamberRepositoy, ChamberRepository>();
            //Business

            //Fluent Validation Custom Error Model Interceptor
            services.AddTransient<IValidatorInterceptor, CustomErrorModelInterceptor>();

        }
    }
}
