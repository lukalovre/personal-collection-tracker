using System.Collections.ObjectModel;
using System.Linq;
using CollectionTracker.Repositories;
using Repositories;

namespace CollectionTracker.ViewModels;

public partial class ComicsViewModel : ItemViewModel<Comic, ComicGridItem, ComicItem>
{
    public ObservableCollection<ComicGridItem> DigitalGridItems { get; } = [];

    public ComicsViewModel(IDatasource datasource, IExternal<Comic> external)
        : base(datasource, external)
    {
        GridItems.CollectionChanged += (_, _) => RefreshDigitalGridItems();
        RefreshDigitalGridItems();
    }

    public override float AmountToMinutesModifier => 0.3f;

    private void RefreshDigitalGridItems()
    {
        var digitalItems = GridItems
            .Where(gridItem => _itemList.Any(item => item.ID == gridItem.ID && item.Format == "Digital"))
            .ToList();

        DigitalGridItems.Clear();
        foreach (var item in digitalItems)
        {
            DigitalGridItems.Add(item);
        }
    }

    public override ComicGridItem Convert(int index, Comic i)
    {
        return new ComicGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Title,
            i.Writer,
            i.Chapter,
            0,
            0
        );
    }

}
