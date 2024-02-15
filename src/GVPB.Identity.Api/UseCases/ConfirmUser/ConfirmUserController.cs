using GVPB.Identity.Application.UseCases.ConfirmUser;
using GVPB.Identity.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GVPB.Identity.Api.UseCases.ConfirmUser;


[ApiController]
[Route("api/[controller]")]
public class ConfirmUserController : ControllerBase
{
    private readonly ConfirmUserPresenter presenter;
    private readonly IConfirmUserUseCase confirmUserUseCase;
    private readonly LanguageManager<SharedResources> languageService;

    public ConfirmUserController
        (ConfirmUserPresenter presenter, 
        IConfirmUserUseCase confirmUserUseCase, 
        LanguageManager<SharedResources> languageService)
    {
        this.presenter = presenter;
        this.confirmUserUseCase = confirmUserUseCase;
        this.languageService = languageService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public IActionResult ConfirmUser([FromBody] ConfirmUser.ConfirmUserRequest confirmUserRequest)
    {
        confirmUserUseCase.Execute(new() { Id = confirmUserRequest.Id, localizer = languageService });
        return presenter.ViewModel;
    }
}
