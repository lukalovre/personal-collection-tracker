using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class StatsView : UserControl
{
    public StatsView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}