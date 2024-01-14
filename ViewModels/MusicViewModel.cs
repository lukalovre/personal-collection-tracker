using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using AvaloniaApplication1.Repositories;
using AvaloniaApplication1.ViewModels.GridItems;
using DynamicData;
using ReactiveUI;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class MusicViewModel : ViewModelBase
{
    private readonly IDatasource _datasource;
    private readonly IExternal<Music> _external;
    private MusicGridItem _selectedItem;
    private List<Music> _itemList;
    private string _inputUrl;
    private Music _newMusic;
    private Bitmap? _cover;
    private Bitmap? _newMusicCover;

    private bool _useNewDate;
    private Music _selectedMusic;
    private int _gridCountMusic;
    private int _gridCountMusicTodo1;
    private int _gridCountMusicTodo2;
    private int _gridCountMusicBookmarked;

    public string SearchText { get; set; }

    public bool UseNewDate
    {
        get => _useNewDate;
        set => this.RaiseAndSetIfChanged(ref _useNewDate, value);
    }

    public static ObservableCollection<string> MusicPlatformTypes =>
        new(
            Enum.GetValues(typeof(eMusicPlatformType))
                .Cast<eMusicPlatformType>()
                .Select(v => v.ToString())
        );

    public static ObservableCollection<PersonComboBoxItem> PeopleList =>
        new(PeopleManager.Instance.GetComboboxList());

    public PersonComboBoxItem SelectedPerson { get; set; }

    public ObservableCollection<MusicGridItem> Music { get; set; }
    public ObservableCollection<MusicGridItem> MusicTodo2 { get; set; }
    public ObservableCollection<MusicGridItem> MusicTodo1 { get; set; }
    public ObservableCollection<MusicGridItem> MusicBookmarked { get; set; }
    public ObservableCollection<MusicGridItem> ArtistMusic { get; set; }

    public Music SelectedMusic
    {
        get => _selectedMusic;
        set => this.RaiseAndSetIfChanged(ref _selectedMusic, value);
    }

    public ReactiveCommand<Unit, Unit> AddClick { get; }
    public ReactiveCommand<Unit, Unit> OpenLink { get; }
    public ReactiveCommand<Unit, Unit> OpenImage { get; }
    public ReactiveCommand<Unit, Unit> ListenAgain { get; }
    public ReactiveCommand<Unit, Unit> Search { get; }

    public Music NewMusic
    {
        get => _newMusic;
        set => this.RaiseAndSetIfChanged(ref _newMusic, value);
    }

    public DateTime NewDate { get; set; } =
        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

    public TimeSpan NewTime { get; set; } = new TimeSpan();

    public string InputUrl
    {
        get => _inputUrl;
        set
        {
            this.RaiseAndSetIfChanged(ref _inputUrl, value);
            InputUrlChanged();
        }
    }

    public Bitmap? Cover
    {
        get => _cover;
        private set => this.RaiseAndSetIfChanged(ref _cover, value);
    }

    public Bitmap? NewMusicCover
    {
        get => _newMusicCover;
        private set => this.RaiseAndSetIfChanged(ref _newMusicCover, value);
    }

    public int GridCountMusic
    {
        get => _gridCountMusic;
        private set => this.RaiseAndSetIfChanged(ref _gridCountMusic, value);
    }

    public int GridCountMusicTodo1
    {
        get => _gridCountMusicTodo1;
        private set => this.RaiseAndSetIfChanged(ref _gridCountMusicTodo1, value);
    }

    public int GridCountMusicTodo2
    {
        get => _gridCountMusicTodo2;
        private set => this.RaiseAndSetIfChanged(ref _gridCountMusicTodo2, value);
    }

    public int GridCountMusicBookmarked
    {
        get => _gridCountMusicBookmarked;
        private set => this.RaiseAndSetIfChanged(ref _gridCountMusicBookmarked, value);
    }

    public MusicViewModel(IDatasource datasource, IExternal<Music> external)
    {
        _datasource = datasource;
        _external = external;

        Music = [];
        MusicTodo2 = [];
        MusicTodo1 = [];
        MusicBookmarked = [];
        ReloadData();

        ArtistMusic = [];

        AddClick = ReactiveCommand.Create(AddClickAction);
        OpenLink = ReactiveCommand.Create(OpenLinkAction);
        OpenImage = ReactiveCommand.Create(OpenImageAction);

        Search = ReactiveCommand.Create(SearchAction);

        SelectedItem = Music.LastOrDefault();
    }

    private void InputUrlChanged()
    {
        NewMusic = _external.GetItem(InputUrl);

        NewMusicCover = FileRepsitory.GetImageTemp<Music>();

        FindMusic(NewMusic);

        _inputUrl = string.Empty;
    }

    private void FindMusic(Music music)
    {
        ArtistMusic.Clear();
        ArtistMusic.AddRange(LoadArtistData(music));
    }

    public MusicGridItem SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            SelectedItemChanged();
        }
    }

    private void SearchAction()
    {
        SearchText = SearchText.Trim();

        if (string.IsNullOrWhiteSpace(SearchText))
        {
            Music.Clear();
            Music.AddRange(LoadData());
            return;
        }

        var searchMusic = new Music { Artist = SearchText, Title = SearchText };

        Music.Clear();
        Music.AddRange(LoadArtistData(searchMusic));
    }

    private void OpenImageAction() { }

    private void OpenLinkAction()
    {
        var openLinkParams = SelectedMusic.Artist.Split(' ').ToList();
        openLinkParams.AddRange(SelectedMusic.Title.Split(' '));
        openLinkParams.AddRange(new string[] { SelectedMusic.Year.ToString() });

        HtmlHelper.OpenLink(SelectedMusic.SpotifyID, [.. openLinkParams]);
    }

    private void AddClickAction()
    {

        _datasource.Add(NewMusic);

        ReloadData();
        ClearNewItemControls();
    }

    private void ReloadData()
    {
        Music.Clear();
        Music.AddRange(LoadData());
        GridCountMusic = Music.Count;

        MusicTodo2.Clear();
        MusicTodo2.AddRange(LoadDataBookmarked(2));
        GridCountMusicTodo2 = MusicTodo2.Count;

        MusicTodo1.Clear();
        MusicTodo1.AddRange(LoadDataBookmarked(1));
        GridCountMusicTodo1 = MusicTodo1.Count;

        MusicBookmarked.Clear();
        MusicBookmarked.AddRange(LoadDataBookmarked());
        GridCountMusicBookmarked = MusicBookmarked.Count;
    }

    private void ClearNewItemControls()
    {
        NewMusic = default;

        NewMusicCover = default;
        InputUrl = default;
        SelectedPerson = default;
        ArtistMusic.Clear();
    }

    private List<MusicGridItem> LoadData()
    {
        _itemList = _datasource.GetList<Music>();

        return [];
    }

    private List<MusicGridItem> LoadDataBookmarked(int? yearsAgo = null)
    {
        _itemList = _datasource.GetList<Music>();

        var dateFilter = yearsAgo.HasValue
            ? DateTime.Now.AddYears(-yearsAgo.Value)
            : DateTime.MaxValue;

        return [];
    }

    private List<MusicGridItem> LoadArtistData(Music item)
    {
        _itemList = _datasource.GetList<Music>();

        return [];
    }

    private static MusicGridItem Convert(int index, Music i)
    {
        return new MusicGridItem(
            i.ID,
            index + 1,
            i.Artist,
            i.Title,
            i.Year,
            i.Runtime,
            false,
            0);
    }

    public void SelectedItemChanged()
    {

        Cover = null;

        if (SelectedItem == null)
        {
            return;
        }

        SelectedMusic = _itemList.First(o => o.ID == SelectedItem.ID);

        var item = _itemList.First(o => o.ID == SelectedItem.ID);
        Cover = FileRepsitory.GetImage<Music>(item.ID);
    }
}
