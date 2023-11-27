using System.Linq.Expressions;
using GVPB.Identity.Domain.Models;

namespace GVPB.Identity.Application.UseCases.ListUser;

public class ListUserRequest
{
    public required Func<User,bool> expression {get; init;}
}

public class ListUserComunications : IComunications
{
    public List<User> Users {get; set;} = new();
}

