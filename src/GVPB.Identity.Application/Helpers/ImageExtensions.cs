namespace GVPB.Identity.Application;

public static class ImageExtensions
{
    public static string ConvertImageToBase64String(string imagePath)
    {
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        return Convert.ToBase64String(imageBytes);
    }
}
