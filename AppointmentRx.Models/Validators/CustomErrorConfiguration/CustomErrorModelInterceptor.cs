using System.Text.Json;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentRx.Models.Validators.CustomErrorConfiguration
{
    public class CustomErrorModelInterceptor : IValidatorInterceptor
    {
        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }

        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext,
            ValidationResult result)
        {
            var failures = result.Errors
                .Select(error => new ValidationFailure(error.PropertyName, SerializeError(error)));

            return new ValidationResult(failures);
        }

        private static string SerializeError(ValidationFailure failure)
        {
            string property = failure.PropertyName;
            property = Char.ToLowerInvariant(property[0]) + property[1..]; //to make property names camelCase
            var error = new Error(property, failure.ErrorMessage);

            return JsonSerializer.Serialize(error);
        }
    }
}
