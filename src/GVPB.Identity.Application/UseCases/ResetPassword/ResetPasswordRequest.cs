
using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Domain;

namespace GVPB.Identity.Application.UseCases.ResetPassword;

public class ResetPasswordRequest
{
    public required Guid IdRequest { get; init; }
    public required string NewPassword { get; init; }
    public required ILanguageManager Localizer { get; init; }
}

public class ResetPasswordComunications : IComunications
{
    public Domain.Models.RequestUser? requestUser { get; set; }
    public required IOutputPort<ResetPasswordResponse> outputPort { get; init; }
}

