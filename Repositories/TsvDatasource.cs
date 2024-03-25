using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace Repositories;

internal class TsvDatasource : IDatasource
{
    private readonly CsvConfiguration _config =
        new(CultureInfo.InvariantCulture) { Delimiter = "\t" };

    public void Add<T>(T item)
        where T : IItem
    {
        var items = GetList<T>();

        if (!items.Any(o => o.ID == item.ID))
        {
            var maxItemID = items.MaxBy(o => o.ID)?.ID ?? 0;
            item.ID = maxItemID + 1;

            var itemFilePath = GetFilePath<T>();

            using var writerItem = new StreamWriter(itemFilePath, true);
            using var csvItem = new CsvWriter(writerItem, _config);

            // if (item.ID == 1)
            // {
            //     // Start of file, write header first
            //     csvItem.WriteHeader<T>();
            // }

            csvItem.NextRecord();
            csvItem.WriteRecord(item);
            FileRepsitory.MoveTempImage<T>(item.ID);
        }

        if (!FileRepsitory.ImageExists<T>(item.ID))
        {
            FileRepsitory.MoveTempImage<T>(item.ID);
        }
    }

    private static string GetFilePath<T>()
    {
        var tableName = GetDataName<T>();
        return Path.Combine(Paths.Data, $"{tableName}.tsv");
    }

    private static string GetEventFilePath<T>()
    {
        var tableName = GetDataName<T>();
        return Path.Combine(Paths.EventDataPath, $"{tableName}.tsv");
    }

    private static string? GetDataName<T>()
    {
        var tAttribute = (TableAttribute)
            typeof(T)?.GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault();
        var tableName = tAttribute?.Name;
        return tableName;
    }

    public List<T> GetList<T>()
        where T : IItem
    {
        var itemFilePath = GetFilePath<T>();

        if (!File.Exists(itemFilePath))
        {
            return [];
        }

        var text = File.ReadAllText(itemFilePath);

        var reader = new StringReader(text);

        // Use _config once all old tsvs are converted
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "\t",
            HasHeaderRecord = false,
            MissingFieldFound = null,
            BadDataFound = null
        };

        using var csv = new CsvReader(reader, config);
        return csv.GetRecords<T>().ToList();
    }

    public void MakeBackup(string path)
    {
        throw new System.NotImplementedException();
    }

    public void Update<T>(T item)
        where T : IItem
    {
        var events = string.Empty;

        // var items = GetList<Game>();

        // foreach (var i in items)
        // {
        //     var oldPath = Path.Combine(Paths.Images, typeof(Game).ToString(), $"{i.Igdb}.png");
        //     ;
        //     var newPath = Path.Combine(Paths.Images, "Game_fixed", $"{i.ID}.png");
        //     ;

        //     if (!File.Exists(oldPath))
        //     {
        //         continue;
        //     }

        //     File.Move(oldPath, newPath);
        // }

        // var bookmarkedItemIDs = new List<int> { 8064, 11221, 16368, 16413, 17480, 17485, 17486 };

        // foreach (var e in events)
        // {
        //     if (bookmarkedItemIDs.Contains(e.ItemID))
        //     {
        //         e.Bookmakred = true;
        //     }
        //     else
        //     {
        //         e.Bookmakred = false;
        //     }
        // }

        // foreach (var i in GetList<Work>())
        // {
        //     var lastEvent = events.Where(o => o.ItemID == i.ID).MaxBy(o => o.DateEnd.Value);

        //     if (!bookmarkedItemIDs.Contains(i.ID))
        //     {
        //         lastEvent.Bookmakred = false;
        //     }
        // }

        // var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        // {
        //     HasHeaderRecord = false,
        //     Delimiter = "\t"
        // };

        var itemFilePath = GetEventFilePath<T>();
        using var writer = new StreamWriter(itemFilePath, false, System.Text.Encoding.UTF8);
        using var csvText = new CsvWriter(writer, _config);
        csvText.WriteRecords(events);
        writer.Flush();
    }

    public List<T> GetDoneList<T>() where T : IExternalItem
    {
        var itemFilePath = GetEventFilePath<T>();

        if (!File.Exists(itemFilePath))
        {
            return [];
        }

        var text = File.ReadAllText(itemFilePath);

        var reader = new StringReader(text);

        // Use _config once all old tsvs are converted
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "\t",
            HasHeaderRecord = false,
            MissingFieldFound = null,
            BadDataFound = null
        };

        using var csv = new CsvReader(reader, config);
        return csv.GetRecords<T>().ToList();
    }
}
