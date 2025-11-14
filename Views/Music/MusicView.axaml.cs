using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class MusicView : UserControl
{
    public MusicView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
