using System;
using System.Collections.Generic;
using System.Linq;
using DynamicData;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Repositories;
using SkiaSharp;

namespace AvaloniaApplication1.ViewModels;

public partial class StatsViewModel : ViewModelBase
{
    private readonly IDatasource _datasource;

    public List<Axis> XAxes { get; set; } = [];

    public List<BarChartInfo> Books { get; }
    public List<BarChartInfo> Games { get; }
    public List<BarChartInfo> Music { get; }
    public List<BarChartInfo> Songs { get; }
    public List<BarChartInfo> TVShows { get; }
    public List<BarChartInfo> Movies { get; }
    public List<BarChartInfo> Comics { get; }
    public List<BarChartInfo> Standup { get; }
    public List<BarChartInfo> Magazine { get; }
    public List<BarChartInfo> Works { get; }

    public StatsViewModel(IDatasource datasource)
    {
        _datasource = datasource;

        var startYear = 2010;
        var endYear = DateTime.Now.Year;
        var yearLabels = Enumerable.Range(startYear, endYear - startYear + 1).Select(o => o.ToString()).ToList();

        XAxes.Add(
            new Axis
            {
                Labels = ["Category 1", "Category 2", "Category 3"],
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

        Books = GetInfo<Book>();
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

    private List<BarChartInfo> GetInfo<T>() where T : ICollection, IItem
    {
        var items = _datasource.GetList<T>();

        var startYear = 2010;
        var endYear = DateTime.Now.Year;

        var result = new List<BarChartInfo>();

        for (int i = startYear; i <= endYear; i++)
        {
            var year = i;

            var totalPrice = items.
                Where(o => o.Date.HasValue && o.Date.Value.Year == year)
                .Sum(o => o.PriceInRSD)
                / 117f;

            var barChartInfo = new BarChartInfo
            {
                Year = year,
                Value = (int)totalPrice!
            };

            result.Add(barChartInfo);
        }

        return result;
    }

    public class BarChartInfo
    {
        public int Year { get; set; }
        public int Value { get; set; }
    }

    public ISeries[] Series { get; set; } =
     [
            new ColumnSeries<double>
            {
                Values = [2, 1, 3, 5, 3, 4, 6]
            }
     ];

}