using Database.Records;

namespace DesktopApp.ViewModels;

internal class MainViewModel : ViewModel
{
    private ViewModel _view = new AuthorizationViewModel();
    private UserRecord? _user;

    public ViewModel View
    {
        get => _view;
        set => Set(ref _view, value);
    }

    public UserRecord? User
    {
        get => _user;
        set => Set(ref _user, value);
    }
}