using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class StandupView : UserControl
{
    public StandupView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
