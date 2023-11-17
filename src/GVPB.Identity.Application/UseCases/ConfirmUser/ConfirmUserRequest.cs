
using GVPB.Identity.Domain.Enum;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.UseCases.ConfirmUser;

public class ConfirmUserRequest
{
    public required Guid Id { get; init; }
}

public class ConfirmUserComunications : IComunications
{
    public RequestUser? requestUser { get; set; }
}

