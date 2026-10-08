using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class DonationsView : UserControl
{
    public DonationsView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}