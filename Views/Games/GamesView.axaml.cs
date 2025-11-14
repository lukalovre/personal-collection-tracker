using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class GamesView : UserControl
{
    public GamesView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
