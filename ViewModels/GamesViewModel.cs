using AvaloniaApplication1.Repositories;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class GamesViewModel : ItemViewModel<Game, GameGridItem>
{
    public GamesViewModel(IDatasource datasource, IExternal<Game> external)
    : base(datasource, external)
    {
    }

    public override GameGridItem Convert(int index, Game i)
    {
        return new GameGridItem(
            i.ID,
            index + 1,
            i.Title,
            i.Year,
            i.Platform,
            i.HLTB,
            i.Date);
    }
}
