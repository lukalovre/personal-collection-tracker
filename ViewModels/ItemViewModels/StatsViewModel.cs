using System;
using System.Collections.Generic;
using System.Linq;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Repositories;
using SkiaSharp;

namespace AvaloniaApplication1.ViewModels;

public partial class StatsViewModel : ViewModelBase
{
    private readonly IDatasource _datasource;
    private readonly List<int> _yearLabels;
    private Dictionary<int, int> _all = [];

    public List<Axis> BooksXAxes { get; set; } = [];
    public List<Axis> GamesXAxes { get; set; } = [];
    public List<Axis> MusicXAxes { get; set; } = [];
    public List<Axis> SongsXAxes { get; set; } = [];
    public List<Axis> TVShowsXAxes { get; set; } = [];
    public List<Axis> MoviesXAxes { get; set; } = [];
    public List<Axis> ComicsXAxes { get; set; } = [];
    public List<Axis> StandupXAxes { get; set; } = [];
    public List<Axis> MagazineXAxes { get; set; } = [];
    public List<Axis> WorksXAxes { get; set; } = [];

    public List<ISeries> Books { get; set; } = [];
    public List<ISeries> Games { get; } = [];
    public List<ISeries> Music { get; } = [];
    public List<ISeries> Songs { get; } = [];
    public List<ISeries> TVShows { get; } = [];
    public List<ISeries> Movies { get; } = [];
    public List<ISeries> Comics { get; } = [];
    public List<ISeries> Standup { get; } = [];
    public List<ISeries> Magazine { get; } = [];
    public List<ISeries> Works { get; } = [];

    public List<Axis> AllXAxes { get; set; } = [];
    public List<ISeries> All { get; } = [];

    public StatsViewModel(IDatasource datasource)
    {
        _datasource = datasource;

        var startYear = 2010;
        var endYear = DateTime.Now.Year;
        _yearLabels = Enumerable.Range(startYear, endYear - startYear + 1).ToList();

        foreach (var item in _yearLabels)
        {
            _all.Add(item, 0);
        }

        FillData<Book>(Books, BooksXAxes);
        FillData<Game>(Games, GamesXAxes);
        FillData<Music>(Music, MusicXAxes);

        FillData<Song>(Songs, SongsXAxes);
        FillData<TVShow>(TVShows, TVShowsXAxes);
        FillData<Movie>(Movies, MoviesXAxes);

        FillData<Comic>(Comics, ComicsXAxes);
        FillData<Standup>(Standup, StandupXAxes);
        FillData<Magazine>(Magazine, MagazineXAxes);

        FillData<Work>(Works, WorksXAxes);

        var color = ChartColors.GetColor("All");

        AllXAxes.Add(
            new Axis
            {
                Labels = _yearLabels.Select(o => o.ToString()).ToList(),
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true
            });

        All.Add(
            new ColumnSeries<int>
            {
                Values = _all.Select(o => o.Value),
                // Stroke = new SolidColorPaint(new SKColor(color.R, color.G, color.B)),
                Fill = new SolidColorPaint(new SKColor(color.R, color.G, color.B))
            });
    }

    private void FillData<T>(List<ISeries> series, List<Axis> xAxes) where T : ICollection, IItem
    {
        var color = ChartColors.GetColor(typeof(T).Name);

        xAxes.Add(
            new Axis
            {
                Labels = _yearLabels.Select(o => o.ToString()).ToList(),
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true
            });

        series.Add(
            new ColumnSeries<int>
            {
                Values = GetInfo<T>(),
                // Stroke = new SolidColorPaint(new SKColor(color.R, color.G, color.B)),
                Fill = new SolidColorPaint(new SKColor(color.R, color.G, color.B))
            });
    }

    private List<int> GetInfo<T>() where T : ICollection, IItem
    {
        var items = _datasource.GetList<T>();

        var startYear = 2010;
        var endYear = DateTime.Now.Year;

        var result = new List<int>();

        for (int i = startYear; i <= endYear; i++)
        {
            var year = i;

            var totalPrice = items.
                Where(o => o.Date.HasValue && o.Date.Value.Year == year)
                .Sum(o => o.PriceInRSD)
                / 117f;

            var totalPriceInt = (int)totalPrice!;

            result.Add(totalPriceInt);

            _all[year] += totalPriceInt;
        }

        return result;
    }
}