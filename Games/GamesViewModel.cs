using System;
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
    public ObservableCollection<GameGridItem> PlatformGames { get; } = [];

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

    private string _selectedPlatform = nameof(eGamePlatformTypes.PC);
    public string SelectedPlatform
    {
        get => _selectedPlatform;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedPlatform, value);
            RefreshPlatformGames();
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
        RefreshPlatformGames();
    }

    private void RefreshClientGames()
    {
        ClientGames.Clear();
        foreach (var game in GridItems.Where(game => string.Equals(NormalizeOption(game.Client), NormalizeOption(SelectedClient), StringComparison.OrdinalIgnoreCase)))
        {
            ClientGames.Add(game);
        }
    }

    private void RefreshPlatformGames()
    {
        PlatformGames.Clear();
        foreach (var game in GridItems.Where(game => string.Equals(NormalizeOption(game.Platform), NormalizeOption(SelectedPlatform), StringComparison.OrdinalIgnoreCase)))
        {
            PlatformGames.Add(game);
        }
    }

    private static string NormalizeOption(string? value)
    {
        return string.Concat((value ?? string.Empty).Where(char.IsLetterOrDigit));
    }
}
