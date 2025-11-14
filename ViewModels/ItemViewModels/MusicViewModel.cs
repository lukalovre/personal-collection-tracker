using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using CollectionTracker.Repositories;
using Repositories;

namespace CollectionTracker.ViewModels;

public partial class MusicViewModel(IDatasource datasource, IExternal<Music> external)
: ItemViewModel<Music, MusicGridItem, MusicItem>(datasource, external)
{

    public override List<string> OpenLinkAlternativeParameters()
    {
        var openLinkParams = SelectedItem.Artist.Split(' ').ToList();
        openLinkParams.AddRange(SelectedItem.Title.Split(' '));
        openLinkParams.AddRange([SelectedItem.Year.ToString()]);

        return openLinkParams;
    }

    public override MusicGridItem Convert(int index, Music i)
    {
        return new MusicGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Artist,
            i.Title,
            i.Year ?? 0,
            0,
            false,
            0);
    }
}

