using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class TVShowsView : UserControl
{
    public TVShowsView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
