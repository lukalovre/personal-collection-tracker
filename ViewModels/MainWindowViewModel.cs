using AvaloniaApplication1.Repositories;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MoviesViewModel MoviesViewModel { get; } = new MoviesViewModel(new TsvDatasource(), new MovieExternal());
    public MusicViewModel MusicViewModel { get; } = new MusicViewModel(new TsvDatasource(), new MusicExternal());
    public WorkViewModel WorkViewModel { get; } = new WorkViewModel(new TsvDatasource());
    public BooksViewModel BooksViewModel { get; } = new BooksViewModel(new TsvDatasource(), new BookExtetrnal());
    public ComicsViewModel ComicsViewModel { get; } = new ComicsViewModel(new TsvDatasource(), new ComicExtetrnal());
    public GamesViewModel GamesViewModel { get; } = new GamesViewModel(new TsvDatasource(), new GameExtetrnal());
    public TVShowsViewModel TVShowsViewModel { get; } = new TVShowsViewModel(new TsvDatasource(), new TVShowExternal());
    public StandupViewModel StandupViewModel { get; } = new StandupViewModel(new TsvDatasource(), new StandupExternal());
    public SongsViewModel SongsViewModel { get; } = new SongsViewModel(new TsvDatasource(), new SongExternal());
    public LibraryViewModel LibraryViewModel { get; } = new LibraryViewModel(new TsvDatasource());
}
