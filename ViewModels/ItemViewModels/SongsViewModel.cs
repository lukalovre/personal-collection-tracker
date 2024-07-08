using AvaloniaApplication1.Repositories;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class SongsViewModel(IDatasource datasource, IExternal<Song> external)
: ItemViewModel<Song, SongGridItem, MusicItem>(datasource, external)
{
    public override SongGridItem Convert(int index, Song i)
    {
        return new SongGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Artist,
            i.Title,
            i.Year ?? 0,
            0,
            false);
    }
}
