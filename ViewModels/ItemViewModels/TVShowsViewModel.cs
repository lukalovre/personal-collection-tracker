using CollectionTracker.Repositories;
using Repositories;

namespace CollectionTracker.ViewModels;

public partial class TVShowsViewModel(IDatasource datasource, IExternal<TVShow> external)
: ItemViewModel<TVShow, TVShowGridItem, MusicItem>(datasource, external)
{
    public override TVShowGridItem Convert(int index, TVShow i)
    {
        return new TVShowGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Title,
            0,
            0,
            null,
            0
        );
    }
}
