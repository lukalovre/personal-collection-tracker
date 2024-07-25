using System;
using AvaloniaApplication1.Models;

public record BookGridItem(int ID, bool Done, string Title, string Author, int Year, int? Rating, int Pages, DateTime? LastDate, string Type, bool? _1001) : IGridItem;
public record ComicGridItem(int ID, bool Done, string Title, string Writer, int? Chapter, int Pages, int? Rating) : IGridItem;
public record GameGridItem(int ID, bool Done, string Title, int Year, string Platform, int HLTB, DateTime? Purchased) : IGridItem;
public record MovieGridItem(int ID, bool Done, string Title, string Director, int Year, int Rating) : IGridItem;
public record MusicGridItem(int ID, bool Done, string Artist, string Title, int Year, int Minutes, bool Bookmarked, int Played) : IGridItem;
public record SongGridItem(int ID, bool Done, string Artist, string Title, int Year, int Times, bool Bookmarked) : IGridItem;
public record TVShowGridItem(int ID, bool Done, string Title, int Season, int Episode, DateTime? LastDate, int DaysAgo) : IGridItem;
public record StandupGridItem(int ID, bool Done, string Title, int Season, int Episode, DateTime? LastDate, int DaysAgo) : IGridItem;
public record WorkGridItem(int ID, bool Done, string Title, string Type, int Minutes, DateTime LastDate) : IGridItem;
public record LibraryGridItem(int ID, string Title, string Type, string Person, DateTime LentDate) : IGridItem;
public record LibrarySearchGridItem(int ID, string Type, string Title) : IGridItem;