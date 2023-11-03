using System.Globalization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.Resources;
using GVPB.Identity.Api;
using GVPB.Identity.Domain;
using GVPB.Identity.Infraestructure.Services;
using Microsoft.AspNetCore.Localization;
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
builder.Services.AddSwaggerGen();
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
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
