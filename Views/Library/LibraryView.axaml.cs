using Avalonia.Controls;

namespace AvaloniaApplication1.Views;

public partial class LibraryView : UserControl
{
    public LibraryView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
