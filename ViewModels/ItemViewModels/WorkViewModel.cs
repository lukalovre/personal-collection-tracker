using System;
using AvaloniaApplication1.ViewModels.GridItems;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class WorkViewModel(IDatasource datasource)
: ItemViewModel<Work, WorkGridItem, WorkItem>(datasource)
{

    public override WorkGridItem Convert(int index, Work i)
    {
        return new WorkGridItem(
            i.ID,
            index + 1,
            i.Title,
            i.Type,
            0,
            DateTime.MinValue
        );
    }

}
