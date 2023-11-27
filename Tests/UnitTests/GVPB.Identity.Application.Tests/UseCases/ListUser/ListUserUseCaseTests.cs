using GVPB.Identity.Application.Interfaces.Database;
using GVPB.Identity.Application.UseCases.ListUser;
using GVPB.Identity.Infraestructure.Tests.Builders;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace GVPB.Identity.Application.Tests.UseCases.ListUser;
[UseAutofacTestFramework]
public class ListUserUseCaseTests
{
    private readonly ListUserPresenter listUserPresenter;
    private readonly IListUserUseCase listUserUseCase;
    private readonly IUserRepository userRepository;

    public ListUserUseCaseTests
    (ListUserPresenter listUserPresenter, 
    IListUserUseCase listUserUseCase, 
    IUserRepository userRepository)
    {
        this.listUserPresenter = listUserPresenter;
        this.listUserUseCase = listUserUseCase;
        this.userRepository = userRepository;
    }

    [Fact]
    public void Should_List_Users_Successfully()
    {
        var user = UserBuilder.New().Build();
        userRepository.Add(user);
        var request = new ListUserRequest { expression = e => e.Id == user.Id };
        listUserUseCase.Execute(request);
    }
}
