﻿
using Autofac;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using GVPB.Identity.Infraestructure.Database;
using ManagementServices.variables.Application.Interfaces;
using ManagementServices.variables.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GVPB.Identity.Infraestructure.Modules;

public class InfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
        .AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<EnvVariableRepository<Context>>().As<IEnvVariableRepository>().InstancePerLifetimeScope();
        Mapper(builder);
        DataAccess(builder);
        base.Load(builder);
    }
    private void DataAccess(ContainerBuilder builder)
    {
        var connection = Environment.GetEnvironmentVariable("DBCONN");

        builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
            .Where(t => (t.Namespace ?? string.Empty).Contains("Database"))
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        if (!string.IsNullOrEmpty(connection))
        {
            try
            {
                using var context = new Context();
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
    private void Mapper(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
          .Where(t => (t.Namespace ?? string.Empty).Contains("Mapper") && typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
          .As<Profile>();
        builder.Register(c => new MapperConfiguration(cfg =>
        {
            foreach (var profile in c.Resolve<IEnumerable<Profile>>())
            {
                cfg.AddProfile(profile);
            }
            cfg.AddExpressionMapping();
        })).AsSelf().SingleInstance();
        builder.Register(c => c.Resolve<MapperConfiguration>()
            .CreateMapper(c.Resolve))
            .As<IMapper>()
            .InstancePerLifetimeScope();
    }
}



