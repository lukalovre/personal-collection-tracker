using Repositories;

namespace CollectionTracker.ViewModels;

public partial class ConsolesViewModel(IDatasource datasource)
: ItemViewModel<ConsoleItem, ConsoleGridItem, WorkItem>(datasource)
{
    protected override bool UsesDoneList => false;

    public override ConsoleGridItem Convert(int index, ConsoleItem item)
    {
        return new ConsoleGridItem(
            item.ID,
            GetDoneStatus(item),
            item.Title,
            item.Year,
            item.Condition,
            item.Type,
            item.Generation,
            item.Controllers,
            item.Games,
            item.Date);
    }
}