using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using CollectionTracker.ViewModels;
using System.Linq;

namespace CollectionTracker.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        AddHandler(Button.ClickEvent, OnButtonClick, RoutingStrategies.Bubble);
    }

    private async void OnButtonClick(object? sender, RoutedEventArgs e)
    {
        if (e.Source is not Button button || button.Content is not Image image || image.Source is not null)
        {
            return;
        }

        if (button.DataContext is not IImageImportTarget target)
        {
            return;
        }

        var isNewItem = image.Name == "NewItemImage";
        if (!target.CanImportImage(isNewItem))
        {
            return;
        }

        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Choose an image",
            AllowMultiple = false,
            FileTypeFilter =
            [
                new FilePickerFileType("Image files")
                {
                    Patterns = ["*.png", "*.jpg", "*.jpeg", "*.bmp", "*.gif", "*.webp", "*.tif", "*.tiff"]
                }
            ]
        });

        var file = files.FirstOrDefault();
        if (file is not null)
        {
            target.ImportImage(file.Path.LocalPath, isNewItem);
        }
    }
}
