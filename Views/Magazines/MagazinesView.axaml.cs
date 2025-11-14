using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class MagazinesView : UserControl
{
    public MagazinesView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
