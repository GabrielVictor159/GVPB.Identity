
using GVPB.Identity.Application.Bundaries;

namespace GVPB.Identity.Application.Tests.Mocks;

public abstract class Presenter<Request> : IOutputPort<Request>
{
    public Request? StandardOutput { get; private set; }
    public string? ErrorMessage { get; private set; }
    public string? NotFoundMessage { get; private set; }

    public void Standard(Request output)
    {
        StandardOutput = output;
    }

    public void Error(string message)
    {
        ErrorMessage = message;
    }

    public void NotFound(string message)
    {
        NotFoundMessage = message;
    }
    
}

