using AvaloniaApplication1.Repositories;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class StandupViewModel(IDatasource datasource, IExternal<Standup> external) : ItemViewModel<Standup, StandupGridItem, MusicItem>(datasource, external)
{
    public override StandupGridItem Convert(int index, Standup i)
    {
        return new StandupGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Title,
            0,
            0,
            null,
            0
        );
    }
}
