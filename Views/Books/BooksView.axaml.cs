using Avalonia.Controls;

namespace CollectionTracker.Views;

public partial class BooksView : UserControl
{
    public BooksView()
    {
        ViewHelper.AddConverters(Resources);
        InitializeComponent();
    }
}
