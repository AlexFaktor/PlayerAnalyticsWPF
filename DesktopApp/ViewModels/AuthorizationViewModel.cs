using Database.Records;
using DesktopApp.Views;
using System.Windows.Input;
using System.Windows;
using DesktopApp.Tools;

namespace DesktopApp.ViewModels;

internal class AuthorizationViewModel : ViewModel
{
    private string _login = string.Empty;
    private string _password = string.Empty;

    public string Login
    {
        get => _login;
        set => Set(ref _login, value);
    }
    public string Password
    {
        get => _password;
        set => Set(ref _password, value);
    }

    public ICommand AuthorizationCommand { get; }
    public ICommand ShowRegistrationCommand { get; }

    public AuthorizationViewModel()
    {
        AuthorizationCommand = new RelayCommand(Authorized);
        ShowRegistrationCommand = new RelayCommand(ShowRegistration);
    }

    // Метод для перемикання
    private void ShowRegistration()
    {
        var _mainWindowVM = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?
        .DataContext as MainViewModel
            ?? throw new Exception("Не вірний DataContext");

        _mainWindowVM.MainView = new RegistrationVM();
    }

    private async void Authorized()
    {
        var app = (App)Application.Current;

        var user = await app.Database.UserRepository.Authorized(new() { Name = _login, Password = Password });
        if (user == null)
        {
            MessageBox.Show("Неправильне введення");
            return;
        }

        if (user != null && (UserRoles)user.Role == UserRoles.Player)
        {
            var _mainWindowVM = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?
            .DataContext as MainWindowVM
            ?? throw new Exception("Не вірний DataContext");

            _mainWindowVM.MainView = new UserMenuVM();
            app.SetUserData(user);
        }
        else if (user != null && (UserRoles)user.Role == UserRoles.Admin)
        {
            var window = Application.Current.Windows.OfType<Window>();

            var _mainWindowVM = window.SingleOrDefault(x => x.IsActive)?
            .DataContext as MainWindowVM
            ?? throw new Exception("Не вірний DataContext");

            _mainWindowVM.MainView = new AdminMenuVM();
            app.SetUserData(user);
        }
    }
}
