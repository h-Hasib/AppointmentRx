using AppointmentRx.Models;
using AppointmentRx.Models.Validators.CustomErrorConfiguration;
using AppointmentRx.WebApi.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;
using System.Text;
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

//Inject Dependency
DependencyServices.Inject(builder.Services, builder.Configuration);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                      .AddJwtBearer(options =>
                      {
                          options.RequireHttpsMetadata = false;
                          options.SaveToken = true;

                          options.TokenValidationParameters =
                               new TokenValidationParameters
                               {
                                   ValidIssuer = builder.Configuration["Tokens:Issuer"],
                                   ValidAudience = builder.Configuration["Tokens:Audience"],
                                   ValidateIssuerSigningKey = true,
                                   ValidateLifetime = true,
                                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Tokens:Key"]))
                               };
                      })
                      .AddCookie(options =>
                      {
                          options.Events = new CookieAuthenticationEvents
                          {
                              OnRedirectToLogin = OnRedirectToLogin,
                              OnRedirectToAccessDenied = OnRedirectToAccessDenied
                          };
                      });

builder.Services.AddDataProtection();
static Task OnRedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> ctx)
{
    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
    {
        ctx.Response.StatusCode = 403;
    }

    return Task.CompletedTask;
}
static Task OnRedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
{
    if (context.Request.Path.StartsWithSegments("/api"))
    {
        // return 401 if not "logged in" from an API Call
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        return Task.CompletedTask;
    }

    // Redirect users to login page
    context.Response.Redirect(context.RedirectUri);
    return Task.CompletedTask;
}

// authorization
builder.Services.AddAuthorization(options =>
{
    // require user to have cookie auth or jwt bearer token
    options.AddPolicy("Authenticated",
        policy => policy
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser());
});

// Add framework services.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        );
});

builder.Services.Configure<MvcOptions>(options =>
{
    options.Filters.Add(new AuthorizeFilter("Authenticated"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Appointment-Rx",
        Version = "v1"
    });
    //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    //{
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.ApiKey,
    //    Scheme = "Bearer",
    //    BearerFormat = "JWT",
    //    In = ParameterLocation.Header,
    //    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    //});
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme {
    //            Reference = new OpenApiReference {
    //                Type = ReferenceType.SecurityScheme,
    //                    Id = "Bearer"
    //            }
    //        },
    //        new string[] {}
    //    }
    //});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
