﻿using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.Bundaries;

public class ConfirmUserResponse
{
    public required User User {get; init;}
}
