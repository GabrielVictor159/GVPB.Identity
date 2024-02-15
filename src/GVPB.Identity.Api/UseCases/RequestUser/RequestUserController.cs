using GVPB.Identity.Api.Helpers;
using GVPB.Identity.Api.UseCases.ConfirmUser;
using GVPB.Identity.Application.UseCases.ConfirmUser;
using GVPB.Identity.Application.UseCases.RequestUser;
using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GVPB.Identity.Api.UseCases.RequestUser;

[ApiController]
[Route("api/[controller]")]
public class RequestUserController : ControllerBase
{
    private readonly RequestUserPresenter presenter;
    private readonly IRequestUserUseCase RequestUserUseCase;
    private readonly LanguageManager<SharedResources> languageService;

    public RequestUserController
        (RequestUserPresenter presenter, 
        IRequestUserUseCase requestUserUseCase, 
        LanguageManager<SharedResources> languageService)
    {
        this.presenter = presenter;
        RequestUserUseCase = requestUserUseCase;
        this.languageService = languageService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("RequestUser")]
    public IActionResult RequestUser([FromBody] RequestUser.RequestUserRequest RequestUserRequest)
    {
        var request = new Application.UseCases.RequestUser.RequestUserRequest()
        {
            NewUser = new Domain.Models.User()
            {
                Id = Guid.NewGuid(),
                UserName = RequestUserRequest.UserName,
                Password = RequestUserRequest.Password,
                Email = RequestUserRequest.Email,
                Rule = Rules.USER
            },
            Localizer = languageService,
            Culture = HttpContext.GetCulture().ToString()
        };
        RequestUserUseCase.Execute(request);
        return presenter.ViewModel;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize(Roles = nameof(Rules.ADMIN))]
    [Route("RequestAdmin")]
    public IActionResult RequestAdmin([FromBody] RequestUser.RequestUserRequest RequestUserRequest)
    {
        var request = new Application.UseCases.RequestUser.RequestUserRequest()
        {
            NewUser = new Domain.Models.User()
            {
                Id = Guid.NewGuid(),
                UserName = RequestUserRequest.UserName,
                Password = RequestUserRequest.Password,
                Email = RequestUserRequest.Email,
                Rule = Rules.ADMIN
            },
            Localizer = languageService,
            Culture = HttpContext.GetCulture().ToString()
        };
        RequestUserUseCase.Execute(request);
        return presenter.ViewModel;
    }
}
