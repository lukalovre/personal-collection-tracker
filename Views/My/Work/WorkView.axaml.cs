using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class WorkView : UserControl
{
    public WorkView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
