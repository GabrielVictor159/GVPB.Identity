
namespace GVPB.Identity.Application.UseCases;

public abstract class Handler<T>
{
    protected Handler<T>? sucessor;
    public dynamic SetSucessor(Handler<T> sucessor)
    {
        this.sucessor = sucessor;
        return this;
    }
    public abstract void ProcessRequest(T request);
}

