using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CollectionTracker.Repositories;
using ReactiveUI;
using Repositories;

namespace CollectionTracker.ViewModels;

public partial class GamesViewModel : ItemViewModel<Game, GameGridItem, GameItem>
{
    public ObservableCollection<GameGridItem> PhysicalCopyGames { get; } = [];
    public ObservableCollection<GameGridItem> ClientGames { get; } = [];

    private string _selectedClient = nameof(eGameClientTypes.GOG);
    public string SelectedClient
    {
        get => _selectedClient;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedClient, value);
            RefreshClientGames();
        }
    }

    public GamesViewModel(IDatasource datasource, IExternal<Game> external)
        : base(datasource, external)
    {
        GridItems.CollectionChanged += (_, _) => RefreshFilteredGameLists();
        RefreshFilteredGameLists();
    }

    public override GameGridItem Convert(int index, Game i)
    {
        return new GameGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Title,
            i.Year,
            i.Platform,
            i.Client,
            i.HLTB,
            i.Date,
            i.PhysicalCopy);
    }

    private void RefreshFilteredGameLists()
    {
        PhysicalCopyGames.Clear();
        foreach (var game in GridItems)
        {
            if (game.PhysicalCopy)
            {
                PhysicalCopyGames.Add(game);
            }
        }

        RefreshClientGames();
    }

    private void RefreshClientGames()
    {
        ClientGames.Clear();
        foreach (var game in GridItems.Where(game => string.Equals(NormalizeClient(game.Client), NormalizeClient(SelectedClient), StringComparison.OrdinalIgnoreCase)))
        {
            ClientGames.Add(game);
        }
    }

    private static string NormalizeClient(string? client)
    {
        return string.Concat((client ?? string.Empty).Where(char.IsLetterOrDigit));
    }
}
