using CollectionTracker.Models;
using CollectionTracker.Repositories;
using Repositories;

namespace CollectionTracker.ViewModels;

public partial class MusicVideosViewModel(IDatasource datasource, IExternal<MusicVideo> external)
: ItemViewModel<MusicVideo, MusicVideoGridItem, MusicItem>(datasource, external)
{
    public override MusicVideoGridItem Convert(int index, MusicVideo i)
    {
        return new MusicVideoGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Author,
            i.Title,
            i.Year,
            i.Runtime,
            false);
    }
}
