using System.IO;
using Avalonia.Media.Imaging;
using AvaloniaApplication1.Repositories;

namespace Repositories;

public class FileRepsitory
{
    public static bool ImageExists<T>(int itemID) where T : IItem
    {
        var filePath = GetImagePath<T>(itemID);
        return File.Exists(filePath);
    }

    public static Bitmap? GetImage<T>(int itemID)
        where T : IItem
    {
        var filePath = GetImagePath<T>(itemID);

        if (!File.Exists(filePath))
        {
            return null;
        }

        return new Bitmap(filePath);
    }

    public static string GetImagePath<T>(int itemID) where T : IItem
    {
        return Path.Combine(Paths.Images, Helpers.GetClassName<T>(), $"{itemID}.png");
    }

    public static Bitmap? GetImageTemp<T>() where T : IItem
    {
        var filePath = Path.Combine($"{Paths.GetTempPath<T>()}.png");

        if (!File.Exists(filePath))
        {
            return null;
        }

        return new Bitmap(filePath);
    }

    public static void Delete(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        File.Delete(filePath);
    }

    public static void MoveTempImage<T>(int itemID)
    {
        var tempFile = $"{Paths.GetTempPath<T>()}.png";
        var destinationFile = Path.Combine(Paths.GetImagesPath<T>(), $"{itemID}.png");

        if (!File.Exists(tempFile))
        {
            return;
        }

        File.Copy(tempFile, destinationFile);
        File.Delete(tempFile);
    }
}
