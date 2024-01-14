using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using AvaloniaApplication1.Repositories;
using DynamicData;
using ReactiveUI;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class ComicsViewModel : ViewModelBase
{
    private const float AMOUNT_TO_MINUTES_MODIFIER = 0.3f;
    private readonly IDatasource _datasource;
    private readonly IExternal<Comic> _external;
    private ComicGridItem _selectedGridItem;
    private List<Comic> _itemList;

    private Comic _newItem;
    private Bitmap? _itemImage;
    private Bitmap? _newItemImage;

    private bool _useNewDate;
    private Comic _selectedItem;
    private int _gridCountItems;

    private int _gridCountItemsBookmarked;
    private int _addAmount;
    private string _addAmountString;

    public int AddAmount
    {
        get => _addAmount;
        set { _addAmount = SetAmount(value); }
    }

    private int _newAmount;
    private string _inputUrl;

    public string AddAmountString
    {
        get => _addAmountString;
        set => this.RaiseAndSetIfChanged(ref _addAmountString, value);
    }

    public bool UseNewDate
    {
        get => _useNewDate;
        set => this.RaiseAndSetIfChanged(ref _useNewDate, value);
    }

    public static ObservableCollection<string> MusicPlatformTypes => [];

    public static ObservableCollection<PersonComboBoxItem> PeopleList =>
        new(PeopleManager.Instance.GetComboboxList());

    public PersonComboBoxItem SelectedPerson { get; set; }

    public ObservableCollection<ComicGridItem> GridItems { get; set; }
    public ObservableCollection<ComicGridItem> GridItemsBookmarked { get; set; }

    public Comic SelectedItem
    {
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
    }

    public DateTime NewDateEnd { get; set; }

    public ReactiveCommand<Unit, Unit> AddItemClick { get; }
    public ReactiveCommand<Unit, Unit> AddEventClick { get; }

    public Comic NewItem
    {
        get => _newItem;
        set => this.RaiseAndSetIfChanged(ref _newItem, value);
    }

    public DateTime NewDate { get; set; } =
        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

    public TimeSpan NewTime { get; set; } = new TimeSpan();

    public Bitmap? Image
    {
        get => _itemImage;
        private set => this.RaiseAndSetIfChanged(ref _itemImage, value);
    }

    public Bitmap? NewImage
    {
        get => _newItemImage;
        private set => this.RaiseAndSetIfChanged(ref _newItemImage, value);
    }

    public int GridCountItems
    {
        get => _gridCountItems;
        private set => this.RaiseAndSetIfChanged(ref _gridCountItems, value);
    }

    public int GridCountItemsBookmarked
    {
        get => _gridCountItemsBookmarked;
        private set => this.RaiseAndSetIfChanged(ref _gridCountItemsBookmarked, value);
    }
    public ComicGridItem SelectedGridItem
    {
        get => _selectedGridItem;
        set
        {
            _selectedGridItem = value;
            SelectedItemChanged();
        }
    }

    public string InputUrl
    {
        get => _inputUrl;
        set
        {
            this.RaiseAndSetIfChanged(ref _inputUrl, value);
            InputUrlChanged();
        }
    }

    public ComicsViewModel(IDatasource datasource, IExternal<Comic> external)
    {
        _datasource = datasource;
        _external = external;

        GridItems = [];
        GridItemsBookmarked = [];
        ReloadData();

        AddItemClick = ReactiveCommand.Create(AddItemClickAction);

        SelectedGridItem = GridItems.LastOrDefault();
    }

    private int SetAmount(int value)
    {

        return value;
    }

    private void InputUrlChanged()
    {
        NewItem = _external.GetItem(InputUrl);

        NewImage = FileRepsitory.GetImageTemp<Comic>();

        _inputUrl = string.Empty;
    }

    private void AddItemClickAction()
    {

        _datasource.Add(NewItem);

        ReloadData();
        ClearNewItemControls();
    }

    private void ReloadData()
    {
        GridItems.Clear();
        GridItems.AddRange(LoadData());
        GridCountItems = GridItems.Count;

        GridItemsBookmarked.Clear();
        GridItemsBookmarked.AddRange(LoadDataBookmarked());
        GridCountItemsBookmarked = GridItemsBookmarked.Count;
    }

    private void ClearNewItemControls()
    {
        NewItem = default;

        NewImage = default;
        SelectedPerson = default;
    }

    private List<ComicGridItem> LoadData()
    {
        _itemList = _datasource.GetList<Comic>();

        return [];
    }

    private List<ComicGridItem> LoadDataBookmarked(int? yearsAgo = null)
    {
        _itemList = _datasource.GetList<Comic>();

        return [];
    }

    private ComicGridItem Convert(int index, Comic i)
    {
        return new ComicGridItem(
            i.ID,
            index + 1,
            i.Title,
            i.Writer,
            1,
            0,
            0
        );
    }

    public void SelectedItemChanged()
    {

        Image = null;

        if (SelectedGridItem == null)
        {
            return;
        }

        SelectedItem = _itemList.First(o => o.ID == SelectedGridItem.ID);

        var item = _itemList.First(o => o.ID == SelectedItem.ID);
        Image = FileRepsitory.GetImage<Comic>(item.ID);
    }
}
