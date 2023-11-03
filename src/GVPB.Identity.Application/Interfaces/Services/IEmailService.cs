namespace GVPB.Identity.Application;

public interface IEmailService
{
    void SendEmail(string To, string Subject, string Image, string? Redirect = null);
}
