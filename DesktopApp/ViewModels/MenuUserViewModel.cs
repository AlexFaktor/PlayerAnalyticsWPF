namespace DesktopApp.ViewModels;

internal class MenuUserViewModel : ViewModel
{
    private ViewModel _selectedControl = new UserProfileViewModel();

    public ViewModel SelectedControl
    {
        get => _selectedControl;
        set => Set(ref _selectedControl, value);
    }

    public MenuUserViewModel()
    {
    }
}
