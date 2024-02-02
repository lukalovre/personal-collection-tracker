using AvaloniaApplication1.Repositories;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class MoviesViewModel(IDatasource datasource, IExternal<Movie> external)
: ItemViewModel<Movie, MovieGridItem, MovieItem>(datasource, external)
{
    public override MovieGridItem Convert(int index, Movie i)
    {
        return new MovieGridItem(
            i.ID,
            index + 1,
            i.Title,
            i.Director,
            i.Year ?? 0,
            0
        );
    }
}
