﻿
namespace GVPB.Identity.Infraestructure;

public class InfrastructureException : Exception
{
    public InfrastructureException(string message)
        : base(message)
    {
    }
}

