using System.Collections.ObjectModel;
using System.Linq;
using CollectionTracker.Repositories;
using Repositories;

namespace CollectionTracker.ViewModels;

public partial class GamesViewModel : ItemViewModel<Game, GameGridItem, GameItem>
{
    public ObservableCollection<GameGridItem> PhysicalCopyGames { get; } = [];

    public GamesViewModel(IDatasource datasource, IExternal<Game> external)
        : base(datasource, external)
    {
        GridItems.CollectionChanged += (_, _) => RefreshPhysicalCopyGames();
        RefreshPhysicalCopyGames();
    }

    public override GameGridItem Convert(int index, Game i)
    {
        return new GameGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Title,
            i.Year,
            i.Platform,
            i.HLTB,
            i.Date,
            i.PhysicalCopy);
    }

    private void RefreshPhysicalCopyGames()
    {
        PhysicalCopyGames.Clear();
        foreach (var game in GridItems.OfType<GameGridItem>().Where(game => game.PhysicalCopy))
        {
            PhysicalCopyGames.Add(game);
        }
    }
}
