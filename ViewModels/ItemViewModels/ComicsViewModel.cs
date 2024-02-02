using AvaloniaApplication1.Repositories;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class ComicsViewModel(IDatasource datasource, IExternal<Comic> external)
: ItemViewModel<Comic, ComicGridItem, ComicItem>(datasource, external)
{
    public override float AmountToMinutesModifier => 0.3f;

    public override ComicGridItem Convert(int index, Comic i)
    {
        return new ComicGridItem(
            i.ID,
            index + 1,
            i.Title,
            i.Writer,
            i.Chapter,
            0,
            0
        );
    }

}
