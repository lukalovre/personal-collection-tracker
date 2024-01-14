using System.Collections.ObjectModel;

namespace AvaloniaApplication1.ViewModels;

public interface IItemViewModel<T>
{

    bool UseNewDate { get; set; }

    static ObservableCollection<PersonComboBoxItem> PeopleList { get; }

    PersonComboBoxItem SelectedPerson { get; set; }

}