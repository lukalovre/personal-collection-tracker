using System;
using System.Collections.Generic;
using System.Linq;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class StatsViewModel : ViewModelBase
{
    public StatsViewModel(IDatasource datasource)
    {
        var books = datasource.GetList<Book>();
        var games = datasource.GetList<Game>();
        var music = datasource.GetList<Music>();
        var songs = datasource.GetList<Song>();
        var tvShows = datasource.GetList<TVShow>();
        var movies = datasource.GetList<Movie>();
        var comics = datasource.GetList<Comic>();
        var standup = datasource.GetList<Standup>();
        var magazines = datasource.GetList<Magazine>();
        var works = datasource.GetList<Work>();

        var collectionDictionary = new Dictionary<string, List<ICollection>>
        {
            { nameof(Book), books.Select(o => o as ICollection).ToList() },
            { nameof(Game), games.Select(o => o as ICollection).ToList() },
            { nameof(Music), music.Select(o => o as ICollection).ToList() },
            { nameof(Song), songs.Select(o => o as ICollection).ToList() },
            { nameof(TVShow), tvShows.Select(o => o as ICollection).ToList() },
            { nameof(Movie), movies.Select(o => o as ICollection).ToList() },
            { nameof(Comic), comics.Select(o => o as ICollection).ToList() },
            { nameof(Standup), standup.Select(o => o as ICollection).ToList() },
            { nameof(Magazine), magazines.Select(o => o as ICollection).ToList() },
            { nameof(Work), works.Select(o => o as ICollection).ToList() }
        };

        var startYear = 2010;
        var endYear = DateTime.Now.Year;

        for (int i = startYear; i <= endYear; i++)
        {
            var year = i;

            foreach (var item in collectionDictionary)
            {
                var booksPrice = item.Value.
                    Where(o => o.Date.HasValue && o.Date.Value.Year == year)
                    .Sum(o => o.PriceInRSD)
                    / 117f;

                booksPrice = (int)booksPrice!;
            }
        }

    }

}