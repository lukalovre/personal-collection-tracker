using System.Linq;
using AvaloniaApplication1.Repositories;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class BooksViewModel(IDatasource datasource, IExternal<Book> external)
: ItemViewModel<Book, BookGridItem, BookItem>(datasource, external)
{
    public override float AmountToMinutesModifier => 2f;

    public override BookGridItem Convert(int index, Book i)
    {

        return new BookGridItem(
            i.ID,
            index + 1,
            i.Title,
            i.Author,
            i.Year ?? 0,
            i.EminaRating,
            i.Pages ?? 0,
            i.Date,
            DoneList?.Contains(i.ExternalID) ?? false
        );
    }
}
