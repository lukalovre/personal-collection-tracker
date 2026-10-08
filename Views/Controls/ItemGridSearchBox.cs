using Avalonia.Controls;
using Avalonia;
using Avalonia.Data;

namespace CollectionTracker.Views;

public partial class ItemGridSearchBox : UserControl
{
    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<ItemGridSearchBox, string>(nameof(Text), string.Empty, defaultBindingMode: BindingMode.TwoWay);

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public ItemGridSearchBox()
    {
        InitializeComponent();
    }
}
