using Database.Records;
using Database.Repositories;
using DesktopApp.Tools;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DesktopApp.ViewModels;

internal class AdminUsersViewModel : ViewModel
{
    private ObservableCollection<UserRecord> _users = [];

    public ObservableCollection<UserRecord> Users
    {
        get => _users;
        set => Set(ref _users, value);
    }

    public ObservableCollection<UserRoles> Roles { get; } =
        new ObservableCollection<UserRoles>((UserRoles[])Enum.GetValues(typeof(UserRoles)));

    public ICommand SaveUserCommand { get; }
    public ICommand DeleteUserCommand { get; }

    private readonly UserRepository _userRepository;

    public AdminUsersViewModel()
    {
        // LoadServices
        var app = App.ServiceProvider;
        _userRepository = app.GetRequiredService<UserRepository>();

        // Load users
        Users = new ObservableCollection<UserRecord>( _userRepository.GetAllAsync().Result);


        SaveUserCommand = new RelayCommand<UserRecord>(SaveUser);
        DeleteUserCommand = new RelayCommand<UserRecord>(DeleteUser);
    }

    private void LoadUsers()
    {
        Users = new ObservableCollection<UserRecord>(_userRepository.GetAllAsync().Result);
    }

    private void SaveUser(UserRecord user)
    {
        LoadUsers();

        _userRepository.UpdateAsync(user);
    }

    private void DeleteUser(UserRecord user)
    {
        LoadUsers();

        if (user != null)
        {
            Users.Remove(user);
            // Example: Delete user from the database
        }
    }
}
