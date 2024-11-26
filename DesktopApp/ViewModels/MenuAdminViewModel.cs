using System.Windows.Controls;

namespace DesktopApp.ViewModels;

internal class MenuAdminViewModel : ViewModel
{
    private ViewModel _selectedControl = new AdminUsersViewModel();

    public ViewModel SelectedControl
    {
        get => _selectedControl;
        set => Set(ref _selectedControl, value);
    }

    public MenuAdminViewModel()
    {
    }
}
