using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class MagazinesViewModel(IDatasource datasource) : ItemViewModel<Magazine, MagazineGridItem, MagazineItem>(datasource)
{
    public override float AmountToMinutesModifier => 2f;

    public override MagazineGridItem Convert(int index, Magazine i)
    {
        return new MagazineGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Title,
            i.Issue,
            i.Date);
    }
}
