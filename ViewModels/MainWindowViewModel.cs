using CollectionTracker.Repositories;
using Repositories;

namespace CollectionTracker.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MoviesViewModel MoviesViewModel { get; } = new MoviesViewModel(new TsvDatasource(), new MovieExternal());
    public MusicViewModel MusicViewModel { get; } = new MusicViewModel(new TsvDatasource(), new MusicExternal());
    public WorkViewModel WorkViewModel { get; } = new WorkViewModel(new TsvDatasource());
    public BooksViewModel BooksViewModel { get; } = new BooksViewModel(new TsvDatasource(), new BookExternal());
    public MagazinesViewModel MagazinesViewModel { get; } = new MagazinesViewModel(new TsvDatasource());
    public ComicsViewModel ComicsViewModel { get; } = new ComicsViewModel(new TsvDatasource(), new ComicExternal());
    public GamesViewModel GamesViewModel { get; } = new GamesViewModel(new TsvDatasource(), new GameExternal());
    public TVShowsViewModel TVShowsViewModel { get; } = new TVShowsViewModel(new TsvDatasource(), new TVShowExternal());
    public StandupViewModel StandupViewModel { get; } = new StandupViewModel(new TsvDatasource(), new StandupExternal());
    public SongsViewModel SongsViewModel { get; } = new SongsViewModel(new TsvDatasource(), new SongExternal());
    public MusicVideosViewModel MusicVideosViewModel { get; } = new MusicVideosViewModel(new TsvDatasource(), new MusicVideoExternal());
    public ClipsViewModel ClipsViewModel { get; } = new ClipsViewModel(new TsvDatasource(), new ClipsExternal());
    public LibraryViewModel LibraryViewModel { get; } = new LibraryViewModel(new TsvDatasource());
    public StatsViewModel StatsViewModel { get; } = new StatsViewModel(new TsvDatasource());
}
