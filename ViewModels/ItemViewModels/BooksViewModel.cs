using System;
using System.Collections.ObjectModel;
using System.Linq;
using CollectionTracker.Repositories;
using ReactiveUI;
using Repositories;

namespace CollectionTracker.ViewModels;

public partial class BooksViewModel : ItemViewModel<Book, BookGridItem, BookItem>
{
    public ObservableCollection<string> BookTypeOptions { get; } = ["Beletristika", "Stručna", "Dečija", "Ostalo"];
    public ObservableCollection<BookGridItem> BeletristikaGridItems { get; } = [];
    public ObservableCollection<BookGridItem> DecijaGridItems { get; } = [];
    public ObservableCollection<BookGridItem> OstaloGridItems { get; } = [];
    public ObservableCollection<BookGridItem> StrucnaGridItems { get; } = [];
    private int _selectedBookTabIndex;

    public int SelectedBookTabIndex
    {
        get => _selectedBookTabIndex;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedBookTabIndex, value);
            RefreshSelectedTypeGridItems();
        }
    }

    public BooksViewModel(IDatasource datasource, IExternal<Book> external)
        : base(datasource, external)
    {
        GridItems.CollectionChanged += (_, _) => RefreshSelectedTypeGridItems();
    }

    public override float AmountToMinutesModifier => 2f;

    private void RefreshSelectedTypeGridItems()
    {
        switch (SelectedBookTabIndex)
        {
            case 2:
                RefreshTypeGridItems(BeletristikaGridItems, "Beletristika");
                break;
            case 3:
                RefreshTypeGridItems(DecijaGridItems, "Dečija");
                break;
            case 4:
                RefreshTypeGridItems(OstaloGridItems, "Ostalo");
                break;
            case 5:
                RefreshTypeGridItems(StrucnaGridItems, "Stručna");
                break;
        }
    }

    private void RefreshTypeGridItems(
        ObservableCollection<BookGridItem> typeGridItems,
        string type)
    {
        var typeItemIds = _itemList
            .Where(item => item.Type == type)
            .Select(item => item.ID)
            .ToHashSet();

        typeGridItems.Clear();
        foreach (var gridItem in GridItems.Where(item => typeItemIds.Contains(item.ID)))
        {
            typeGridItems.Add(gridItem);
        }
    }

    public BookTypes BookTypesCount => GetDoneStatus();

    public record BookTypes
    {
        public int Strucna { get; set; }
        public int Beletristika { get; set; }
        public int Decija { get; set; }
        public int Ostalo { get; set; }
    }

    private BookTypes GetDoneStatus()
    {
        return new BookTypes
        {
            Beletristika = _itemList.ToList().Count(o => o.Type == "Beletristika"),
            Strucna = _itemList.ToList().Count(o => o.Type == "Stručna"),
            Decija = _itemList.ToList().Count(o => o.Type == "Dečija"),
            Ostalo = _itemList.ToList().Count(o => o.Type == "Ostalo"),
        };
    }

    public override BookGridItem Convert(int index, Book i)
    {
        return new BookGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Title,
            i.Author,
            i.Year ?? 0,
            i.EminaRating,
            i.Pages ?? 0,
            i.Date,
            i.Type,
            i._1001 ?? false
        );
    }
}
