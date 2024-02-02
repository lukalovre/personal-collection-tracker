using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using AvaloniaApplication1.Repositories;
using AvaloniaApplication1.ViewModels.GridItems;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

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
            index + 1,
            i.Artist,
            i.Title,
            i.Year ?? 0,
            0,
            false,
            0);
    }
}

