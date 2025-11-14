using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class SongsView : UserControl
{
    public SongsView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
