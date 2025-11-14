using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class LibraryView : UserControl
{
    public LibraryView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
