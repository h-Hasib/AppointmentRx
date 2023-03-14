using AppointmentRx.Models.Validators.CustomErrorConfiguration;
using FluentValidation.AspNetCore;

namespace AppointmentRx.WebApi.DependencyInjection
{
    public static class DependencyServices
    {
        public static void Inject(IServiceCollection services, IConfiguration config)
        {
            //Fluent Validation Custom Error Model Interceptor
            services.AddTransient<IValidatorInterceptor, CustomErrorModelInterceptor>();
        }
    }
}
