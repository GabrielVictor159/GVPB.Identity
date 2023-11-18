using GVPB.Identity.Application.Resources.Images.Factory.Enum;
using System.Text.RegularExpressions;

namespace GVPB.Identity.Application.Resources.Images.Factory;

public static class ApplicationImagesFactory
{
    private static string pathImages = Environment.GetEnvironmentVariable("PATH_IMAGES_APLICATIONS")!;
    public static string? GetBase64Images(ImagesApplication imagesApplication, string culture)
    {
        switch (imagesApplication)
        {
            case ImagesApplication.CONFIRM_USER:
                {

                    return getImages(culture, "Confirm_User.png");
                }
            default:
                return null;

        }
    }
    private static string getImages(string culture, string imageName)
    {
        return ImageExtensions.ConvertImageToBase64String($"{pathImages}{culture.ToUpper()}/{imageName}");
    }
}

