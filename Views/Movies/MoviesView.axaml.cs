using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class MoviesView : UserControl
{
    public MoviesView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
