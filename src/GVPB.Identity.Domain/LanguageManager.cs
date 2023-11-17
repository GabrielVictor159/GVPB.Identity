using Microsoft.Extensions.Localization;

namespace GVPB.Identity.Domain;
public interface ILanguageManager
{
   LocalizedString GetKey(string Key);
}
public class LanguageManager<T> : ILanguageManager
{
   public IStringLocalizer? localizer;
   private IStringLocalizerFactory factory;

   public LanguageManager(IStringLocalizerFactory factory)
   {
        this.factory = factory;
   }
   public LocalizedString GetKey(string Key)
   {
    this.localizer = factory.Create(typeof(T));
    return localizer[Key];
   }
}
