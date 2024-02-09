using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.UseCases.RecoverPassword;

public class RecoverPasswordRequest
{
    public required string Email { get; init; }
    public required ILanguageManager localizer { get; init; }
}

public class RecoverPasswordComunications : IComunications
{
    public User? user { get; set; }
    public required IOutputPort<RecoverPasswordResponse> outputPort { get; init; }
}
