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

public partial class LibraryViewModel : ItemViewModel<Library, LibraryGridItem, MusicItem>
{

    public LibraryViewModel(IDatasource datasource) : base(datasource)
    {
        ReturnedClick = ReactiveCommand.Create(ReturnedClickAction);
        LendItemClick = ReactiveCommand.Create(LendItemClickAction);
    }

    private void LendItemClickAction()
    {
        throw new NotImplementedException();
    }

    private void ReturnedClickAction()
    {
        throw new NotImplementedException();
    }

    public ObservableCollection<LibraryGridItem> LibraryGridItems { get; set; } = [];
    public List<Tuple<string, IItem>> ItemList { get; set; } = [];

    public object ReturnedClick { get; private set; }
    public ReactiveCommand<Unit, Unit> LendItemClick { get; private set; }

    protected override async Task ReloadData(string searchText = null)
    {
        ItemList.Clear();

        ItemList.AddRange(_datasource.GetList<Book>().Select(o => new Tuple<string, IItem>(typeof(Book).Name, o)));
        ItemList.AddRange(_datasource.GetList<Comic>().Select(o => new Tuple<string, IItem>(typeof(Comic).Name, o)));
        ItemList.AddRange(_datasource.GetList<Game>().Select(o => new Tuple<string, IItem>(typeof(Game).Name, o)));

        LibraryGridItems.Clear();
        LibraryGridItems.AddRange(await LoadData());

        await base.ReloadData(searchText);
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

    public override LibraryGridItem Convert(int index, Library i)
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

    protected override Bitmap? GetItemImage(LibraryGridItem selectedGridItem)
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
