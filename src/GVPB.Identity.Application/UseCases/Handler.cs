
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GVPB.Identity.Application.UseCases;

public abstract class Handler<T, F>
{
    protected Handler<T, F?>? sucessor;
    public dynamic SetSucessor(Handler<T, F?> sucessor)
    {
        this.sucessor = sucessor;
        return this;
    }
    public abstract void ProcessRequest(T request, F? comunications = default(F?));
  

}

