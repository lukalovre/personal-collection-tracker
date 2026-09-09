using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class MusicVideosView : UserControl
{
    public MusicVideosView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
