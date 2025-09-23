using System;
using System.Linq;
using AvaloniaApplication1.Repositories;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class BooksViewModel(IDatasource datasource, IExternal<Book> external)
: ItemViewModel<Book, BookGridItem, BookItem>(datasource, external)
{
    public override float AmountToMinutesModifier => 2f;

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
