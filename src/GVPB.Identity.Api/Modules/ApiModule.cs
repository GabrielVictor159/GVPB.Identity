﻿using Autofac;
using GVPB.Identity.Domain;

namespace GVPB.Identity.Api;

public class ApiModule: Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(ApiException).Assembly)
                 .AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();
    }

}
