using System.Globalization;
using ResxGen;

string mergedResourcesPath = "../GVPB.Identity.Api/Resources";
var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("pt"),
    new CultureInfo("es"),
};
List<string> inputDirectories = new List<string>
        {
            "../GVPB.Identity.Domain/Resources"
        };
new ResxMerger(supportedCultures,mergedResourcesPath, inputDirectories).Merge("SharedResources");
Console.WriteLine("Finished!");
