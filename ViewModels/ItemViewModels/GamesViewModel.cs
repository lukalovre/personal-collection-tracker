using AvaloniaApplication1.Repositories;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class GamesViewModel(IDatasource datasource, IExternal<Game> external)
: ItemViewModel<Game, GameGridItem, GameItem>(datasource, external)
{
    public override GameGridItem Convert(int index, Game i)
    {
        return new GameGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Title,
            i.Year,
            i.Platform,
            i.HLTB,
            i.Date);
    }
}
