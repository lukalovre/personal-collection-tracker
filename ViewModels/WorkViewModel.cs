using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia.Media.Imaging;
using AvaloniaApplication1.ViewModels.GridItems;
using DynamicData;
using ReactiveUI;
using Repositories;

namespace AvaloniaApplication1.ViewModels;

public partial class WorkViewModel : ViewModelBase
{
    private readonly IDatasource _datasource;
    private WorkGridItem _selectedItem;
    private List<Work> _itemList;
    private string _inputUrl;
    private Work _newWork;
    private Bitmap? _cover;
    private Bitmap? _newMusicCover;

    private bool _useNewDate;
    private Work _selectedWork;
    private int _gridCountMusic;

    private int _gridCountMusicBookmarked;

    public int AddMinutes { get; set; }

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

    public ObservableCollection<WorkGridItem> Work { get; set; }
    public ObservableCollection<WorkGridItem> WorkBookmarked { get; set; }

    public Work SelectedWork
    {
        get => _selectedWork;
        set => this.RaiseAndSetIfChanged(ref _selectedWork, value);
    }

    public ReactiveCommand<Unit, Unit> AddClick { get; }
    public ReactiveCommand<Unit, Unit> ListenAgain { get; }

    public Work NewWork
    {
        get => _newWork;
        set => this.RaiseAndSetIfChanged(ref _newWork, value);
    }

    public DateTime NewDate { get; set; } =
        new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

    public TimeSpan NewTime { get; set; } = new TimeSpan();

    public Bitmap? Image
    {
        get => _cover;
        private set => this.RaiseAndSetIfChanged(ref _cover, value);
    }

    public Bitmap? NewImage
    {
        get => _newMusicCover;
        private set => this.RaiseAndSetIfChanged(ref _newMusicCover, value);
    }

    public int GridCountWork
    {
        get => _gridCountMusic;
        private set => this.RaiseAndSetIfChanged(ref _gridCountMusic, value);
    }

    public int GridCountWorkBookmarked
    {
        get => _gridCountMusicBookmarked;
        private set => this.RaiseAndSetIfChanged(ref _gridCountMusicBookmarked, value);
    }

    public WorkViewModel(IDatasource datasource)
    {
        _datasource = datasource;

        Work = [];
        WorkBookmarked = [];
        ReloadData();

        AddClick = ReactiveCommand.Create(AddClickAction);

        SelectedItem = Work.LastOrDefault();
    }

    public WorkGridItem SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            SelectedItemChanged();
        }
    }

    private void OpenImageAction() { }

    private void AddClickAction()
    {

        _datasource.Add(NewWork);

        ReloadData();
        ClearNewItemControls();
    }

    private void ReloadData()
    {
        Work.Clear();
        Work.AddRange(LoadData());
        GridCountWork = Work.Count;

        WorkBookmarked.Clear();
        WorkBookmarked.AddRange(LoadDataBookmarked());
        GridCountWorkBookmarked = WorkBookmarked.Count;
    }

    private void ClearNewItemControls()
    {
        NewWork = default;
        NewImage = default;
        SelectedPerson = default;
    }
    private List<WorkGridItem> LoadData()
    {
        _itemList = _datasource.GetList<Work>();

        return [];
    }

    private List<WorkGridItem> LoadDataBookmarked(int? yearsAgo = null)
    {
        _itemList = _datasource.GetList<Work>();

        var dateFilter = yearsAgo.HasValue
            ? DateTime.Now.AddYears(-yearsAgo.Value)
            : DateTime.MaxValue;

        return [];
    }

    private static WorkGridItem Convert(int index, Work i)
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

    public void SelectedItemChanged()
    {
        Image = null;

        if (SelectedItem == null)
        {
            return;
        }

        SelectedWork = _itemList.First(o => o.ID == SelectedItem.ID);

        var item = _itemList.First(o => o.ID == SelectedItem.ID);
        Image = FileRepsitory.GetImage<Work>(item.ID);
    }
}
