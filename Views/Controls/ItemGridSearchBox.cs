using Avalonia.Controls;
using Avalonia;
using Avalonia.Data;

namespace CollectionTracker.Views;

public partial class ItemGridSearchBox : UserControl
{
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<ItemGridSearchBox, string>(nameof(Text), string.Empty, defaultBindingMode: BindingMode.TwoWay);

    public static readonly StyledProperty<int> CountProperty =
        AvaloniaProperty.Register<ItemGridSearchBox, int>(nameof(Count));

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public int Count
    {
        get => GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }

    public ItemGridSearchBox()
    {
        InitializeComponent();
    }
}
