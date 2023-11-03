using FluentValidation;
using Microsoft.Extensions.Localization;

namespace GVPB.Identity.Domain.Validator;

public abstract class LocalizatorValidator<TValidator, TModel> : AbstractValidator<TModel>, ICloneable
{
    protected ILanguageManager? Localizer;
    public LocalizatorValidator(ILanguageManager? localizer = null)
    {
        this.Localizer = localizer;
        Configure();
    }

    protected abstract void Configure();
    protected string WithMessageLocalizer(string Id, string Message)
    {
        if(Localizer == null)
        {
            return Message;
        }
        else
        {
            var test = Localizer.GetKey(Id);
            return Localizer.GetKey(Id).Value;
        }
    }
    public object Clone()
    {
        return MemberwiseClone();
    }
}
