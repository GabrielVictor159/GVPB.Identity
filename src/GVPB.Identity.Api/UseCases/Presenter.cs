using GVPB.Identity.Application.Bundaries;
using GVPB.Identity.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GVPB.Identity.Api.UseCases;

public abstract class Presenter<Request, Response> : IOutputPort<Request>
{
    public IActionResult ViewModel { get; private set; } = new ObjectResult(new { StatusCode = 500 });
    private readonly LanguageManager<SharedResources> languageService;
    public Presenter(LanguageManager<SharedResources> languageService)
    {
        this.languageService = languageService;
    }

    public void Error(string message)
    {
        var problemdetails = new ProblemDetails()
        {
            Status = 500,
            Detail = message
        };
        ViewModel = new BadRequestObjectResult(problemdetails);
    }

    public void NotFound(string message)
    {
        ViewModel = new NotFoundObjectResult(message);
    }

    public void Standard(Request request)
    {
        var response = Activator.CreateInstance(typeof(Response), request, languageService);
        this.ViewModel = new OkObjectResult(response);
    }
}
