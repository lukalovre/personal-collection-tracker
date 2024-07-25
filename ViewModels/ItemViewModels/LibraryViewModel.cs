using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using DynamicData;
using ReactiveUI;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class LibraryViewModel : ViewModelBase
{
    public LibraryViewModel(IDatasource datasource)
    {
        _datasource = datasource;

        ReturnedClick = ReactiveCommand.Create(ReturnedClickAction);
        LendItemClick = ReactiveCommand.Create(LendItemClickAction);
        Search = ReactiveCommand.Create(SearchAction);

        ReloadData();
    }

    public ReactiveCommand<Unit, Unit> Search { get; }

    private void SearchAction()
    {
        throw new NotImplementedException();
    }

    private void LendItemClickAction()
    {
        throw new NotImplementedException();
    }
    public LibraryGridItem SelectedGridItem
    {
        get => _selectedGridItem;
        set
        {
            _selectedGridItem = value;
            SelectedItemChanged();
        }
    }

    public Bitmap? Image
    {
        get => _itemImage;
        private set => this.RaiseAndSetIfChanged(ref _itemImage, value);
    }

    public string SearchText { get; set; }

    public Library NewItem
    {
        get => _newItem;
        set => this.RaiseAndSetIfChanged(ref _newItem, value);
    }

    private void SelectedItemChanged()
    {
        Image = null;

        if (SelectedGridItem == null)
        {
            return;
        }

        SelectedItem = _itemList.First(o => o.ID == SelectedGridItem.ID);
        Image = GetItemImage(SelectedGridItem);
    }

    public Library SelectedItem
    {
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
    }

    private void ReturnedClickAction()
    {
        throw new NotImplementedException();
    }

    public ObservableCollection<LibraryGridItem> LibraryGridItems { get; set; } = [];

    public ObservableCollection<LibrarySearchGridItem> SearchItems { get; set; } = [];

    public LibrarySearchGridItem SelectedSearchGridItem
    {
        get => _selectedSearchGridItem;
        set
        {
            _selectedSearchGridItem = value;
            SelectedSearchGridItemChanged();
        }
    }

    private void SelectedSearchGridItemChanged()
    {
        throw new NotImplementedException();
    }

    public List<Tuple<string, IItem>> ItemList { get; set; } = [];

    private readonly IDatasource _datasource;
    private List<Library> _itemList = [];
    private LibraryGridItem _selectedGridItem;
    private Bitmap? _itemImage;
    private Library _newItem;
    private Library _selectedItem;
    private LibrarySearchGridItem _selectedSearchGridItem;

    public object ReturnedClick { get; private set; }
    public ReactiveCommand<Unit, Unit> LendItemClick { get; private set; }

    private async Task ReloadData(string searchText = null)
    {
        ItemList.Clear();

        ItemList.AddRange(_datasource.GetList<Book>().Select(o => new Tuple<string, IItem>(typeof(Book).Name, o)));
        ItemList.AddRange(_datasource.GetList<Comic>().Select(o => new Tuple<string, IItem>(typeof(Comic).Name, o)));
        ItemList.AddRange(_datasource.GetList<Game>().Select(o => new Tuple<string, IItem>(typeof(Game).Name, o)));

        LibraryGridItems.Clear();
        LibraryGridItems.AddRange(await LoadData());

        SearchItems.Clear();

        foreach (var item in ItemList)
        {
            var title = item.Item2.Title ?? string.Empty;

            if (item.Item2 is Comic comic)
            {
                title = $"{title} {comic.Chapter}";
            }

            var gridItem = new LibrarySearchGridItem(item.Item2.ID, item.Item1, title);
            SearchItems.Add(gridItem);
        }
    }

    private async Task<List<LibraryGridItem>> LoadData()
    {
        _itemList = _datasource.GetList<Library>();

        return _itemList
            .Where(o => o.ReturnDate is null)
            .OrderByDescending(o => o.Date)
            .Select((o, i) => Convert(i, o))
            .ToList();
    }

    public LibraryGridItem Convert(int index, Library i)
    {
        var tuple = ItemList.FirstOrDefault(o => o.Item1 == i.Type && o.Item2.ID == i.ItemID);

        if (tuple == null)
        {
            return default!;
        }

        var title = tuple.Item2.Title ?? string.Empty;

        if (tuple.Item2 is Comic comic)
        {
            title = $"{title} {comic.Chapter}";
        }

        return new LibraryGridItem(
            i.ID,
            title,
            i.Type,
            PeopleManager.Instance.GetDisplayName(i.PersonID),
            i.LentDate);
    }

    protected Bitmap? GetItemImage(LibraryGridItem selectedGridItem)
    {
        var libraryItem = _itemList.First(o => o.ID == selectedGridItem.ID);
        var item = ItemList.First(o => o.Item1 == libraryItem.Type && o.Item2.ID == libraryItem.ItemID).Item2;

        return selectedGridItem.Type switch
        {
            nameof(Book) => FileRepsitory.GetImage<Book>(item.ID),
            nameof(Comic) => FileRepsitory.GetImage<Comic>(item.ID),
            nameof(Game) => FileRepsitory.GetImage<Game>(item.ID),
            _ => null,
        };
    }
}
