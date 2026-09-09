using CollectionTracker.Models;
using CollectionTracker.Repositories;
using Repositories;

namespace CollectionTracker.ViewModels;

public partial class ClipsViewModel(IDatasource datasource, IExternal<Clip> external)
: ItemViewModel<Clip, ClipGridItem, Clip>(datasource, external)
{
    protected override bool UsesDoneList => false;

    public override ClipGridItem Convert(int index, Clip i)
    {
        return new ClipGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Author,
            i.Title,
            i.Year,
            i.Runtime,
            false);
    }
}
