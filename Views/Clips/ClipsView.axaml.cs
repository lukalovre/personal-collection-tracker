using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class ClipsView : UserControl
{
    public ClipsView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
