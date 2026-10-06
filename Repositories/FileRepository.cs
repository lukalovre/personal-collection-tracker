using System.IO;
using Avalonia.Media.Imaging;
using CollectionTracker.Repositories;

namespace Repositories;

public class FileRepository
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

    public static void ImportImage<T>(string sourcePath, int itemID) where T : IItem
    {
        var destinationPath = Path.Combine(Paths.GetImagesPath<T>(), $"{itemID}.png");
        SaveAsPng(sourcePath, destinationPath);
    }

    public static void ImportTempImage<T>(string sourcePath) where T : IItem
    {
        var destinationPath = $"{Paths.GetTempPath<T>()}.png";
        SaveAsPng(sourcePath, destinationPath);
    }

    private static void SaveAsPng(string sourcePath, string destinationPath)
    {
        using var bitmap = new Bitmap(sourcePath);
        using var destination = File.Create(destinationPath);
        bitmap.Save(destination);
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
