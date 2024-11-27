using Database.Records;
using Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DesktopApp.ViewModels;

internal class UserProfileViewModel : ViewModel
{
    private string _userName;
    public PlayerStatisticRecord PlayerStatisticRecord { get; set; }

    public string UserName
    {
        get => _userName;
        set => Set(ref _userName, value);
    }

    public UserProfileViewModel()
    {
        var user = App.CurrentUser!;
        _userName = user.Name;
        PlayerStatisticRecord = App.ServiceProvider.GetRequiredService<PlayerStatisticRepository>().GetByUserId(user.Id).Result;
    }
}

