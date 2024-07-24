using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DynamicData;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class LibraryViewModel(IDatasource datasource) : ItemViewModel<Library, LibraryGridItem, MusicItem>(datasource)
{

    public ObservableCollection<LibraryGridItem> LibraryGridItems { get; set; } = [];

    protected override async Task ReloadData(string searchText = null)
    {
        LibraryGridItems.Clear();
        LibraryGridItems.AddRange(await LoadData());

        await base.ReloadData(searchText);
    }

    private async Task<List<LibraryGridItem>> LoadData()
    {
        var _itemList = datasource.GetList<Library>();

        return _itemList
            .Where(o => o.ReturnDate is null)
            .OrderByDescending(o => o.Date)
            .Select((o, i) => Convert(i, o))
            .ToList();
    }

    public override LibraryGridItem Convert(int index, Library i)
    {
        return new LibraryGridItem(
            i.ID,
            i.Title,
            i.Type,
            PeopleManager.Instance.GetDisplayName(i.PersonID),
            i.LentDate);
    }
}
