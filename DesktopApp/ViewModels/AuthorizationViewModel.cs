using Database.Records;
using DesktopApp.Views;
using System.Windows.Input;
using System.Windows;
using DesktopApp.Tools;
using Microsoft.Extensions.DependencyInjection;
using Database.Repositories;

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

        _mainWindowVM.View = new RegistrationViewModel();
    }

    private async void Authorized()
    {
        var userRepository = App.ServiceProvider.GetRequiredService<UserRepository>();

        var user = await userRepository.Authorize(_login, _password);

        if (user == null || (_login == string.Empty || _password ==string.Empty))
        {
            MessageBox.Show("Неправильне введення");
            return;
        }

        var mainVM = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?
            .DataContext as MainViewModel ?? throw new Exception("Не вірний DataContext");

        if (user != null && (UserRoles)user.Role == UserRoles.Player)
        {
            mainVM.View = new MenuUserViewModel();
        }
        else if (user != null && (UserRoles)user.Role == UserRoles.Admin)
        {
            mainVM.View = new MenuAdminViewModel();
        }

        mainVM.User = user;
    }
}
