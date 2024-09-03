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
    private readonly List<string> _yearLabels;

    public List<Axis> BooksXAxes { get; set; } = [];

    public List<ISeries> Books { get; set; } = [];

    public List<int> Games { get; }
    public List<int> Music { get; }
    public List<int> Songs { get; }
    public List<int> TVShows { get; }
    public List<int> Movies { get; }
    public List<int> Comics { get; }
    public List<int> Standup { get; }
    public List<int> Magazine { get; }
    public List<int> Works { get; }

    public StatsViewModel(IDatasource datasource)
    {
        _datasource = datasource;

        var startYear = 2010;
        var endYear = DateTime.Now.Year;
        _yearLabels = Enumerable.Range(startYear, endYear - startYear + 1).Select(o => o.ToString()).ToList();

        FillData<Book>(Books, BooksXAxes);

        Games = GetInfo<Game>();
        Music = GetInfo<Music>();
        Songs = GetInfo<Song>();
        TVShows = GetInfo<TVShow>();
        Movies = GetInfo<Movie>();
        Comics = GetInfo<Comic>();
        Standup = GetInfo<Standup>();
        Magazine = GetInfo<Magazine>();
        Works = GetInfo<Work>();
    }

    private void FillData<T>(List<ISeries> series, List<Axis> xAxes) where T : ICollection, IItem
    {
        xAxes.Add(
            new Axis
            {
                Labels = _yearLabels,
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                TicksAtCenter = true,
                // By default the axis tries to optimize the number of 
                // labels to fit the available space, 
                // when you need to force the axis to show all the labels then you must: 
                ForceStepToMin = true,
                MinStep = 1
            });

        series.Add(new ColumnSeries<int> { Values = GetInfo<T>() });
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

            result.Add((int)totalPrice!);
        }

        return result;
    }
}