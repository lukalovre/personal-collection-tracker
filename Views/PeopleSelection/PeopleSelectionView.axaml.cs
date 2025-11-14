using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class PeopleSelectionView : UserControl
{
    public PeopleSelectionView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
