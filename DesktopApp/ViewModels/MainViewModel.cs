using Database.Records;

namespace DesktopApp.ViewModels;

internal class MainViewModel : ViewModel
{
    private ViewModel _view = new AuthorizationViewModel();

    public ViewModel View
    {
        get => _view;
        set => Set(ref _view, value);
    }
}