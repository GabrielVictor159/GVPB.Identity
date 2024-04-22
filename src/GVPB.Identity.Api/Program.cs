using System.Diagnostics;
using System.Globalization;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.Resources;
using GVPB.Identity.Api;
using GVPB.Identity.Api.DependencyInjection;
using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Infraestructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var supportedCultures = new[] 
{
    new CultureInfo("en"),
    new CultureInfo("pt"),
    new CultureInfo("es"),
};
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.AddAutofacRegistration());
builder.Services.AddSingleton<LanguageManager<SharedResources>>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Adicionar esquema de autentica��o JWT ao Swagger UI
    var securityScheme = new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    };
    c.AddSecurityRequirement(securityRequirement);
});
builder.Services.AddFilters();
Environment.SetEnvironmentVariable("SECRET", Guid.NewGuid().ToString());
var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET")!);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Cliente", policy => policy.RequireRole(Rules.USER.ToString()));
    options.AddPolicy("Admin", policy => policy.RequireRole(Rules.ADMIN.ToString()));
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(e =>
{
    e.RequireHttpsMetadata = false;
    e.SaveToken = true;
    e.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.WebHost.UseUrls("http://*:5031");

var app = builder.Build();

var requestLocalizationOptions = new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

app.UseRequestLocalization(requestLocalizationOptions);
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}
app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
