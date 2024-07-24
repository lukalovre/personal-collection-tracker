using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class LibraryViewModel(IDatasource datasource) : ItemViewModel<Library, LibraryGridItem, MusicItem>(datasource)
{
    public override LibraryGridItem Convert(int index, Library i)
    {
        return new LibraryGridItem(
            i.ID,
            i.Title,
            i.Type,
            i.LentTo,
            PeopleManager.Instance.GetDisplayName(i.PersonID),
            i.LentDate);
    }
}
