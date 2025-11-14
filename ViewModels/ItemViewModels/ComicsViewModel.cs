using CollectionTracker.Repositories;
using Repositories;

namespace CollectionTracker.ViewModels;

public partial class ComicsViewModel(IDatasource datasource, IExternal<Comic> external)
: ItemViewModel<Comic, ComicGridItem, ComicItem>(datasource, external)
{
    public override float AmountToMinutesModifier => 0.3f;

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
