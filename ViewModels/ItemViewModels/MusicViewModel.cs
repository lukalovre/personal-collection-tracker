using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using CollectionTracker.Repositories;
using Repositories;

namespace CollectionTracker.ViewModels;

public partial class MusicViewModel : ItemViewModel<Music, MusicGridItem, MusicItem>
{
    public ObservableCollection<MusicGridItem> DigitalGridItems { get; } = [];
    public ObservableCollection<MusicGridItem> CDGridItems { get; } = [];
    public ObservableCollection<MusicGridItem> CassetteGridItems { get; } = [];
    public ObservableCollection<MusicGridItem> VinylGridItems { get; } = [];

    public MusicViewModel(IDatasource datasource, IExternal<Music> external)
        : base(datasource, external)
    {
        GridItems.CollectionChanged += (_, _) => RefreshPlatformGridItems();
        RefreshPlatformGridItems();
    }

    private void RefreshPlatformGridItems()
    {
        RefreshPlatformGridItems(DigitalGridItems, eMusicPlatformType.Digital);
        RefreshPlatformGridItems(CDGridItems, eMusicPlatformType.CD);
        RefreshPlatformGridItems(CassetteGridItems, eMusicPlatformType.Cassette);
        RefreshPlatformGridItems(VinylGridItems, eMusicPlatformType.Vinyl);
    }

    private void RefreshPlatformGridItems(
        ObservableCollection<MusicGridItem> platformGridItems,
        eMusicPlatformType platform)
    {
        var platformItemIds = _itemList
            .Where(item => FormatMatchesPlatform(item.Format, platform))
            .Select(item => item.ID)
            .ToHashSet();

        platformGridItems.Clear();
        foreach (var gridItem in GridItems.Where(item => platformItemIds.Contains(item.ID)))
        {
            platformGridItems.Add(gridItem);
        }
    }

    private static bool FormatMatchesPlatform(string format, eMusicPlatformType platform)
    {
        return platform == eMusicPlatformType.Digital
            ? format.StartsWith("Digital", StringComparison.Ordinal)
            : format == platform.ToString();
    }

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

