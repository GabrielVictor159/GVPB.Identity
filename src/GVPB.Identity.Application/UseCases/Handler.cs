
using GVPB.Identity.Application.Interfaces.Database;
using Newtonsoft.Json;

namespace GVPB.Identity.Application.UseCases;

public abstract class Handler<T, F> : ICloneable where F : IComunications
{
    protected Handler<T, F>? sucessor;
    private List<dynamic> objects = new List<dynamic>();
    private bool BreakFlux {get;set;} = false; 
    public dynamic SetSucessor(Handler<T, F> sucessor)
    {
        this.sucessor = sucessor;
        return this;
    }
    protected abstract void ProcessRequest(T request, F? comunications = default(F?));

    protected void SetObjectsLog(params dynamic[] items)
    {
        List<dynamic> dynamicList = new List<dynamic>(items);
        foreach (var item in dynamicList)
        {
            objects.Add(item);
        }
    }
    protected void Break()
    {
        BreakFlux = true;
    }
    public void Execute(T request, F? comunications = default(F?))
    {
        var className = Clone().GetType().FullName;

        comunications?.AddLog(Domain.Enum.LogType.Process,$"Executing {className}");

        ProcessRequest(request,comunications);

        if(objects.Any())
        {
        comunications?.AddLog(Domain.Enum.LogType.Information,$"{className} Objects: {JsonConvert.SerializeObject(objects)}");
        }
        if(!BreakFlux)
        {
        sucessor?.Execute(request,comunications);
        }
    }

    public object Clone()
    {
        return MemberwiseClone();
    }
}

