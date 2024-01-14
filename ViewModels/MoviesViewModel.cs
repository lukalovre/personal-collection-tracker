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

public partial class MoviesViewModel : ViewModelBase
{
    private readonly IDatasource _datasource;
    private readonly IExternal<Movie> _external;
    private MovieGridItem _selectedGridItem;
    private List<Movie> _itemList;
    private Movie _newItem;
    private Bitmap? _itemImage;
    private Bitmap? _newItemImage;

    private bool _useNewDate;
    private Movie _selectedItem;
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

    public PersonComboBoxItem? SelectedPerson { get; set; }

    public ObservableCollection<MovieGridItem> GridItems { get; set; }
    public ObservableCollection<MovieGridItem> GridItemsBookmarked { get; set; }

    public Movie SelectedItem
    {
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
    }

    public string SearchText { get; set; }

    public ReactiveCommand<Unit, Unit> AddItemClick { get; }
    public ReactiveCommand<Unit, Unit> AddEventClick { get; }
    public ReactiveCommand<Unit, Unit> Search { get; }

    public Movie NewItem
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
    public MovieGridItem SelectedGridItem
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

    public MoviesViewModel(IDatasource datasource, IExternal<Movie> external)
    {
        _datasource = datasource;
        _external = external;

        GridItems = [];
        GridItemsBookmarked = [];
        ReloadData();

        AddItemClick = ReactiveCommand.Create(AddItemClickAction);

        Search = ReactiveCommand.Create(SearchAction);

        SelectedGridItem = GridItems.LastOrDefault();
    }

    private void InputUrlChanged()
    {
        NewItem = _external.GetItem(InputUrl);
        NewImage = FileRepsitory.GetImageTemp<Movie>();

        _inputUrl = string.Empty;
    }

    private void SearchAction()
    {
        SearchText = SearchText.Trim();

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            GridItemsBookmarked.Clear();
            GridItemsBookmarked.AddRange(LoadData());
            return;
        }

        var searchMovie = new Movie { Director = SearchText, Title = SearchText };

        GridItemsBookmarked.Clear();
        GridItemsBookmarked.AddRange(LoadArtistData(searchMovie));
    }

    private IEnumerable<MovieGridItem> LoadArtistData(Movie searchMovie)
    {
        _itemList = _datasource.GetList<Movie>();

        return [];
    }

    private int SetAmount(int value)
    {
        _addAmount = value;
        AddAmountString = $"    Adding {_addAmount} minutes";
        return value;
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
        GridItemsBookmarked.AddRange(LoadDataBookmarked(0));
        GridCountItemsBookmarked = GridItemsBookmarked.Count;
    }

    private void ClearNewItemControls()
    {
        NewItem = new Movie();

        NewImage = default;
        SelectedPerson = default;
    }

    private List<MovieGridItem> LoadData()
    {
        _itemList = _datasource.GetList<Movie>();

        return [];
    }

    private List<MovieGridItem> LoadDataBookmarked(int? yearsAgo = null)
    {
        _itemList = _datasource.GetList<Movie>();

        var dateFilter = yearsAgo.HasValue
            ? DateTime.Now.AddYears(-yearsAgo.Value)
            : DateTime.MaxValue;

        return [];
    }

    private static MovieGridItem Convert(int index, Movie i)
    {

        return new MovieGridItem(
            i.ID,
            index + 1,
            i.Title,
            i.Director,
            i.Year,
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
        Image = FileRepsitory.GetImage<Movie>(item.ID);
    }
}
