using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Repositories;
using DynamicData;
using ReactiveUI;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public class ItemViewModel<TItem, TGridItem, TEventItem> : ViewModelBase
where TItem : IItem
where TGridItem : IGridItem
where TEventItem : IExternalItem
{
    public virtual float AmountToMinutesModifier => 1f;
    private readonly IDatasource _datasource;
    private readonly IExternal<TItem> _external;
    private TGridItem _selectedGridItem;
    private List<TItem> _itemList;
    private TItem _newItem;
    private Bitmap? _itemImage;
    private Bitmap? _newItemImage;

    private TItem _selectedItem;
    private int _gridCountItems;

    private int _gridCountItemsBookmarked;
    private int _addAmount;
    private string _addAmountString;
    private string _inputUrl;

    public int AddAmount
    {
        get => _addAmount;
        set { _addAmount = SetAmount(value); }
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

    public string AddAmountString
    {
        get => _addAmountString;
        set => this.RaiseAndSetIfChanged(ref _addAmountString, value);
    }

    public static ObservableCollection<string> MusicPlatformTypes =>
        new(
            Enum.GetValues(typeof(eGamePlatformTypes))
                .Cast<eGamePlatformTypes>()
                .Select(v => v.ToString())
        );

    public static ObservableCollection<PersonComboBoxItem> PeopleList =>
        new(PeopleManager.Instance.GetComboboxList());

    public PersonComboBoxItem SelectedPerson { get; set; }

    public ObservableCollection<TGridItem> GridItems { get; set; }
    public ObservableCollection<TGridItem> GridItemsTodo { get; set; }
    public ObservableCollection<TGridItem> GridItemsBookmarked { get; set; }

    public TItem SelectedItem
    {
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
    }

    public ReactiveCommand<Unit, Unit> AddItemClick { get; }
    public ReactiveCommand<Unit, Unit> IgnoreItemClick { get; }
    public ReactiveCommand<Unit, Unit> AddEventClick { get; }
    public ReactiveCommand<Unit, Unit> Search { get; }
    public ReactiveCommand<Unit, Unit> OpenLink { get; }

    public TItem NewItem
    {
        get => _newItem;
        set => this.RaiseAndSetIfChanged(ref _newItem, value);
    }

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
    public TGridItem SelectedGridItem
    {
        get => _selectedGridItem;
        set
        {
            _selectedGridItem = value;
            SelectedItemChanged();
        }
    }

    public string SearchText { get; set; }

    public ItemViewModel(IDatasource datasource, IExternal<TItem> external = null!)
    {
        _datasource = datasource;
        _external = external;

        GridItems = [];
        GridItemsTodo = [];
        GridItemsBookmarked = [];
        ReloadData();

        AddItemClick = ReactiveCommand.Create(AddItemClickAction);
        IgnoreItemClick = ReactiveCommand.Create(IgnoreItemClickAction);
        Search = ReactiveCommand.Create(SearchAction);
        OpenLink = ReactiveCommand.Create(OpenLinkAction);

        SelectedGridItem = GridItems.LastOrDefault();
        NewItem = (TItem)Activator.CreateInstance(typeof(TItem));
    }

    public virtual List<string> OpenLinkAlternativeParameters()
    {
        return [];
    }

    private void OpenLinkAction()
    {
        HtmlHelper.OpenLink(SelectedItem.ExternalID, OpenLinkAlternativeParameters());
    }

    private void SearchAction()
    {
        SearchText = SearchText.Trim();

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            GridItemsTodo.Clear();
            GridItemsTodo.AddRange(LoadData());
            return;
        }

        // var searchMovie = new Movie { Director = SearchText, Title = SearchText };

        GridItemsTodo.Clear();
        GridItemsTodo.AddRange(LoadDataTodo());
    }

    public async void InputUrlChanged()
    {
        NewItem = await _external.GetItem(InputUrl);
        NewImage = FileRepsitory.GetImageTemp<TItem>();

        _inputUrl = string.Empty;
    }

    private int SetAmount(int value)
    {
        _addAmount = value;
        AddAmountString = $"    Adding {_addAmount} minutes";
        return value;
    }

    private void AddItemClickAction()
    {
        NewItem.Date = NewItem.Date ?? DateTime.Now;
        _datasource.Add(NewItem);

        ReloadData();
        ClearNewItemControls();
    }

    private void IgnoreItemClickAction()
    {

        if (SelectedItem is Book book)
        {
            book.Bookmarked = false;
            _datasource.Update(book);
            ReloadData();
            ClearNewItemControls();
        }
    }

    private void ReloadData()
    {
        GridItems.Clear();
        GridItems.AddRange(LoadData());
        GridCountItems = GridItems.Count;

        GridItemsTodo.Clear();
        GridItemsTodo.AddRange(LoadDataTodo());
        GridCountItemsBookmarked = GridItemsTodo.Count;

        GridItemsBookmarked.Clear();
        GridItemsBookmarked.AddRange(LoadDataBookmarked());
        GridCountItemsBookmarked = GridItemsBookmarked.Count;
    }

    private void ClearNewItemControls()
    {
        NewItem = (TItem)Activator.CreateInstance(typeof(TItem));
        NewImage = default;
        SelectedPerson = default;
    }

    private List<TGridItem> LoadData()
    {
        _itemList = _datasource.GetList<TItem>();

        return _itemList
            .OrderBy(o => o.Date)
            .Select((o, i) => Convert(i, o))
            .ToList();
    }

    private List<TGridItem> LoadDataTodo(int? yearsAgo = null)
    {
        _itemList = _datasource.GetList<TItem>();
        var doneList = _datasource.GetDoneList<TEventItem>().Select(o => o.ExternalID);

        return _itemList
        .Where(o => !doneList.Contains(o.ExternalID))
            .OrderBy(o => o.Date)
            .Select((o, i) => Convert(i, o))
            .ToList();
    }

    private List<TGridItem> LoadDataBookmarked(int? yearsAgo = null)
    {
        _itemList = _datasource.GetList<TItem>();
        return _itemList
            .Where(o => (o as Book)?.Bookmarked ?? true)
            .OrderBy(o => o.Date)
            .Select((o, i) => Convert(i, o))
            .ToList();
    }

    public virtual TGridItem Convert(int index, TItem i)
    {
        return default;
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
        Image = FileRepsitory.GetImage<TItem>(item.ID);
    }
}
