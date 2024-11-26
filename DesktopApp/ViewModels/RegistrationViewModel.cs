using DesktopApp.Tools;
using System.Windows.Input;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Database.Repositories;

namespace DesktopApp.ViewModels;

internal class RegistrationViewModel : ViewModel
{
    private string _login;
    private string _password;
    private string _passwordRepeat;

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
    public string PasswordRepeat
    {
        get => _passwordRepeat;
        set => Set(ref _passwordRepeat, value);
    }

    public ICommand ShowAuthorizationCommand { get; }
    public ICommand RegistrationCommand { get; }

    public RegistrationViewModel()
    {
        ShowAuthorizationCommand = new RelayCommand(ShowAuthorization);
        RegistrationCommand = new RelayCommand(Registration);
    }

    public async void Registration()
    {
        if (_password != _passwordRepeat)
        {
            MessageBox.Show("Паролі не співпадають");
            return;
        }
        var userRepository = App.ServiceProvider.GetRequiredService<UserRepository>();

        var possibleUser = await userRepository.GetByNameAsync(_login);
        if (possibleUser != null)
        {
            MessageBox.Show("Користувач вже існує");
            return;
        }

        await userRepository.Register(_login, _password);
    }

    private void ShowAuthorization()
    {
        var _mainWindowVM = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive)?
            .DataContext as MainViewModel ?? throw new Exception("Не вірний DataContext");

        _mainWindowVM.View = new AuthorizationViewModel();
    }
}
