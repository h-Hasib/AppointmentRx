﻿
using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Repositories.Doctor.Appointments;
using AppointmentRx.DataAccess.Repositories.Doctor.Chambers;
using AppointmentRx.DataAccess.Repositories.Doctor.Profile;
using AppointmentRx.DataAccess.Repositories.Patient.Appointment;
using AppointmentRx.DataAccess.Repositories.Patient.DoctorInfo;
using AppointmentRx.DataAccess.Repositories.Patient.FavouriteDoctorRepo;
using AppointmentRx.DataAccess.Repositories.Patient.PatientAppointment;
using AppointmentRx.DataAccess.Repositories.Patient.Profile;
using AppointmentRx.DataAccess.Repositories.User;
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
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IChamberRepositoy, ChamberRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IDoctorInfoRepository, DoctorInfoRepository>();
            services.AddTransient<IFavouriteDoctorRepository, FavouriteDoctorRepository>();
            services.AddTransient<IPatientAppointmentRepository, PatientAppointmentRepository>();
            //Business
            //Fluent Validation Custom Error Model Interceptor
            services.AddTransient<IValidatorInterceptor, CustomErrorModelInterceptor>();
        }
    }
}
