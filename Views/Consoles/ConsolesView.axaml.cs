using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class ConsolesView : UserControl
{
    public ConsolesView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}