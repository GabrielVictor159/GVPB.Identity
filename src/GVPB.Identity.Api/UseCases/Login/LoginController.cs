using GVPB.Identity.Api.Helpers;
using GVPB.Identity.Api.UseCases.RequestUser;
using GVPB.Identity.Application.UseCases.Login;
using GVPB.Identity.Application.UseCases.RequestUser;
using GVPB.Identity.Domain;
using GVPB.Identity.Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GVPB.Identity.Api.UseCases.Login;
[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginPresenter presenter;
    private readonly ILoginUseCase useCase;
    private readonly LanguageManager<SharedResources> languageService;

    public LoginController
        (LoginPresenter presenter,
        ILoginUseCase useCase,
        LanguageManager<SharedResources> languageService)
    {
        this.presenter = presenter;
        this.useCase = useCase;
        this.languageService = languageService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Login([FromBody] Login.LoginRequest request)
    {

        useCase.Execute(new() { Localizer= languageService , Password= request.Password, UserNameOrUserEmail = request.UserNameOrUserEmail});
        return presenter.ViewModel;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    [Route("VerifyToken")]
    public IActionResult VerifyToken()
    {
        return Ok("Authentic Token");
    }
}
