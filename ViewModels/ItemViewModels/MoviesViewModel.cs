using CollectionTracker.Repositories;
using Repositories;

namespace CollectionTracker.ViewModels;

public partial class MoviesViewModel(IDatasource datasource, IExternal<Movie> external) : ItemViewModel<Movie, MovieGridItem, MovieItem>(datasource, external)
{
    public override MovieGridItem Convert(int index, Movie i)
    {
        return new MovieGridItem(
            i.ID,
            GetDoneStatus(i),
            i.Title,
            i.Director,
            i.Year ?? 0,
            0
        );
    }
}
