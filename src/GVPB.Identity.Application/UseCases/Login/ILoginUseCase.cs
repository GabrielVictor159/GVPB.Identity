﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GVPB.Identity.Application.UseCases.Login;
public interface ILoginUseCase
{
    void Execute(LoginRequest request);
}
