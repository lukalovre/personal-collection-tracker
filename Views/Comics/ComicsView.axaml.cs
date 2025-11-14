using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class ComicsView : UserControl
{
    public ComicsView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
