using System;
using Repositories;

namespace CollectionTracker.ViewModels;

public partial class WorkViewModel(IDatasource datasource)
: ItemViewModel<Work, WorkGridItem, WorkItem>(datasource)
{

    public override WorkGridItem Convert(int index, Work i)
    {
        return new WorkGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Title,
            i.Type,
            0,
            DateTime.MinValue
        );
    }

}
