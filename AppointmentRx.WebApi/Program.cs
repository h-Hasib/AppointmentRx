using AppointmentRx.Models;
using AppointmentRx.Models.Validators.CustomErrorConfiguration;
using AppointmentRx.WebApi.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using JsonSerializer = System.Text.Json.JsonSerializer;

var builder = WebApplication.CreateBuilder(args);


//Fluent Validation
builder.Services.AddControllers()
        .ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => JsonSerializer.Deserialize<Error>(e.ErrorMessage));

                var errorResponse = new HttpResponseModel(data: errors, success: false, message: "validation failed."); //In case if you want to represent this result with a response model
                return new BadRequestObjectResult(errorResponse);
            };
        });
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inject Dependency
DependencyServices.Inject(builder.Services, builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
