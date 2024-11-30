using Database.Records;
using Database.Repositories;
using DesktopApp.Tools;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp.ViewModels;

internal class AdminUsersViewModel : ViewModel
{
    private readonly UserRepository _userRepository;

    private ObservableCollection<UserRecord> _users = [];

    public ObservableCollection<UserRecord> Users
    {
        get => _users;
        set => Set(ref _users, value);
    }

    public ObservableCollection<UserRoles> Roles { get; } =
        new ObservableCollection<UserRoles>((UserRoles[])Enum.GetValues(typeof(UserRoles)));

    private string? _filterName = null;

    public string? FilterName
    {
        get => _filterName;
        set => Set(ref _filterName, value);
    }

    private bool? _isAdmin = null;
    public bool? IsAdmin
    {
        get => _isAdmin;
        set => Set(ref _isAdmin, value);
    }

    public ICommand SaveUserCommand { get; }
    public ICommand DeleteUserCommand { get; }
    public ICommand FilterCommand { get; }

    public AdminUsersViewModel()
    {
        // LoadServices
        var app = App.ServiceProvider;
        _userRepository = app.GetRequiredService<UserRepository>();

        // Load users
        Users = new ObservableCollection<UserRecord>( _userRepository.GetAllAsync().Result);


        SaveUserCommand = new RelayCommand<UserRecord>(SaveUser);
        DeleteUserCommand = new RelayCommand<UserRecord>(DeleteUser);
        FilterCommand = new RelayCommand(Filter);
    }

    private void LoadUsers()
    {
        Users = new ObservableCollection<UserRecord>(_userRepository.GetAllUsersAsync( _filterName, _isAdmin).Result);
    }

    private void SaveUser(UserRecord user)
    {
        _userRepository.UpdateAsync(user);
        LoadUsers();
    }

    private void DeleteUser(UserRecord user)
    {
        LoadUsers();

        if (user.Name == App.CurrentUser!.Name)
        {
            MessageBox.Show("Ви не можете видалити себе!", "Увага", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        if (user != null)
        {
            Users.Remove(user);
            _userRepository.DeleteAsync(user.Id);
        }
    }

    private void Filter()
    {
        LoadUsers();
    }
}
